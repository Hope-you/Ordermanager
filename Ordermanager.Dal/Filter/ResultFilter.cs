using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Ordermanager.Model;

namespace Ordermanager.Dal.Filter
{
    public class ResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

            //结果执行之后
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var apiResult = new ApiResult()
            {
                Data = context.Result,
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

    public class ActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            ObjectResult rst = context.Result as ObjectResult;
            object rstValue = rst != null ? rst.Value : null;
            if (context.Exception != null)
            {
                //// 异常处理
                //context.ExceptionHandled = true;
                //if (context.Exception is UserException)
                //{
                //    // 如果是用户异常
                //    context.HttpContext.Response.StatusCode = 200;
                //    context.Result = new ObjectResult(new OperateResult(false, context.Exception.Message, rstValue));
                //    var apiResult = new ApiResult()
                //    {
                //        Data = context.Result,
                //        Msg = context.Exception.Message,
                //        StatusCode = context.HttpContext.Response.StatusCode,
                //    };
                //    context.Result = new ObjectResult();
                //}
            }
        }
        /// <summary>
        /// 方法执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }

    public class UserException : Exception
    {
        /// <summary>
        /// 异常的信息
        /// </summary>
        /// <param name="message"></param>
        public UserException(string message) : base(message)
        {
        }

    }
}
