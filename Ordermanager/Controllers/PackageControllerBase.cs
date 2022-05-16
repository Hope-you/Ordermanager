using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Ordermanager.Model;
using System;
using System.Net;

namespace Ordermanager.Api.Controllers
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

    public class ResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

            //结果执行之后
        }

        public async void OnResultExecuting(ResultExecutingContext context)
        {
            var apiResult = new ApiResult()
            {
                Data = context.Result,
                Msg = "",
                StatusCode = context.HttpContext.Response.StatusCode,
            };
            if (context.Result.GetType() == typeof(ObjectResult))
                apiResult.Data = (context.Result as ObjectResult).Value;


            context.Result = new ContentResult
            {
                Content = JsonConvert.SerializeObject(apiResult),
                StatusCode = context.HttpContext.Response.StatusCode,
                ContentType = "application/json"
            };
        }
    }
}
