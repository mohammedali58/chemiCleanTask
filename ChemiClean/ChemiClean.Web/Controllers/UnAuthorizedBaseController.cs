using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace ChemiClean.Web.Controllers
{
    public class UnAuthorizedBaseController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }

        [NonAction]
        public string GetCulture()
        {
            try
            {
                var HeaderCulture = HttpContext.Request.Headers?.Where(x => x.Key.Equals("Accept-Language"))?.FirstOrDefault();
                if (HeaderCulture is null || !HeaderCulture.HasValue)
                    return Configuration.GetSection("DefaultLan").Value;
                else
                    return HeaderCulture.Value.Value[0]?.ToString();
            }
            catch (Exception)
            {
                return Configuration.GetSection("DefaultLan").Value;
            }
        }
    }
}