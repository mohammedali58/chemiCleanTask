using AutoMapper;
using ChemiClean.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ChemiClean.Core.UseCases
{
    public class BaseUseCase
    {
        #region Props

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public IConfiguration Configuration { get; set; }
        public IMapper Mapper { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public IHttpContextAccessor _httpContext { get; set; }

        #endregion Props

        #region Ctor

        public BaseUseCase()
        {
        }

        #endregion Ctor




    }
}