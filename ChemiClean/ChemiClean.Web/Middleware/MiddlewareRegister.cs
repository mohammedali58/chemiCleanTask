using Microsoft.AspNetCore.Builder;

namespace ChemiClean.Web.Middleware
{
    public static class MiddlewareRegister
    {
        public static void UseAppMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}