using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ChemiClean.Core;
using ChemiClean.Core.Interfaces;
using ChemiClean.Infrastructure;
using ChemiClean.Infrastructure.Mapping;
using ChemiClean.SharedKernel;
using ChemiClean.Web.Controllers;
using ChemiClean.Web.Middleware;
using System;
using System.Linq;


namespace ChemiClean.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private string AllowedOrigins { get; } = "AllowedOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentException(nameof(Configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Add Controllers + As Service To Implment Property Injection

            services.AddControllersWithViews().AddControllersAsServices().AddNewtonsoftJson();
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddMvcCore().AddDataAnnotations();


            #endregion  

            services.AddDbContext<ChemicleanContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DBConString")));

            #region AutoMapper

            services.AddAutoMapper(typeof(ChemiCleanMapping).Assembly);

            #endregion AutoMapper

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChemiClean API", Version = "v1" });

                c.DescribeAllParametersInCamelCase();
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            },
                //            Scheme = "oauth2",
                //            Name = "Bearer",
                //            In = ParameterLocation.Header,
                //        },
                //        new List<string>()
                //    }
                //});
            });

            #endregion 

            #region CORS

            IConfigurationSection originsSection = Configuration.GetSection(AllowedOrigins);
            var origns = originsSection.AsEnumerable()
                .Where(s => s.Value != null).Select(a => a.Value).ToArray();
            services.AddCors(options =>
            {
                options.AddPolicy(AllowedOrigins,
                        builder =>
                        {
                            builder.WithOrigins(origns)
                                   .AllowAnyMethod()
                                   .AllowAnyHeader()
                                   .AllowCredentials();
                        });
            });

            #endregion  

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));


            #region Request Length (For Attachments)

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            #endregion

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            #region Register DB Context

            builder.Register(c =>
            {
                return new ChemicleanContext(new DbContextOptionsBuilder<ChemicleanContext>()
                    .UseSqlServer(Configuration.GetConnectionString("DBConString")).Options);
            }).InstancePerLifetimeScope();

            #endregion

            #region Register Unit Of Work

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            #endregion 

            #region Register Shared Kernel Classes

            builder.RegisterType<HtmlFileReader>().As<IHtmlFileReader>().InstancePerLifetimeScope();
            #endregion  

            #region Register The Genric Output Port

            builder.RegisterGeneric(typeof(OutputPort<>)).PropertiesAutowired();
            builder.RegisterGeneric(typeof(OutputPort<>)).As(typeof(IOutputPort<>)).InstancePerLifetimeScope().PropertiesAutowired();

            #endregion  

            #region Register HttpContextAccessor In Order To Access The Http Context Inside A Class Library (Chemiclean.Core Project)

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance().PropertiesAutowired();

            #endregion


            #region Register All Repositories & UseCases
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).PublicOnly().Where(t => t.IsClass && t.Name.ToLower().EndsWith("usecase")).AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).PublicOnly().Where(t => t.IsClass && t.Name.ToLower().EndsWith("repository")).AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();

            #endregion  

            #region Register Controller For Property DI

            Type controllerBaseType = typeof(UnAuthorizedBaseController);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType).PropertiesAutowired();

            Type _controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).Where(t => _controllerBaseType.IsAssignableFrom(t) && t != _controllerBaseType).PropertiesAutowired();

            #endregion  

       

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region CORS

            app.UseCors(AllowedOrigins);

            #endregion CORS

            #region StaticFiles

            app.UseStaticFiles();

            #endregion StaticFiles

            #region AppBuilder

            app.UseAppMiddleware();
            app.UseHttpsRedirection();
            app.UseRouting();


            #endregion AppBuilder

            #region Swagger

            IConfigurationSection SwaggerBasePath = Configuration.GetSection("SwaggerBasePath");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{SwaggerBasePath.Value}/swagger/v1/swagger.json", "ChemiClean Api V1");
                c.RoutePrefix = string.Empty;
            });

            #endregion Swagger

            #region EndPoints

            // app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            #endregion EndPoints
        }
    }
}
