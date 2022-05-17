using Microsoft.AspNetCore.Mvc;

namespace Ordermanager.Api.Controllers.BaseController
{

    /// <summary>
    /// 封装了ControllerBase
    /// 如果有别的所有控制器都需要的，可以在这里添
    /// </summary>
    public class PackageControllerBase : ControllerBase
    {
        /// <summary>
        /// 如果用户传递过了token
        /// </summary>
        public string _userId => Response.HttpContext.User.Identity.Name;
    }
}
