using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Ordermanager.Model
{

    public class ActionSimpResult
    {
        /// <summary>
        /// 是否是成功的,状态码是200也可能是失败的，这里指的是业务上的
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 信息，控制反馈的数据
        /// 可能是异常信息，也可能是空
        /// </summary>
        public string Msg { get; set; }


        private object data;
        /// <summary>
        /// 数据实体
        /// </summary>
        public object Data
        {
            get => data;
            set
            {

                var type = value.GetType();
                if (type == typeof(string))
                {
                    Success = string.IsNullOrEmpty((string)value);
                }

                if (type == typeof(bool))
                {
                    Success = (bool)value;
                }
                //默认msg的内容是是否请求成功，判断标准就是Success是否成功
                Msg = Success ? "请求成功" : "请求失败";
                data = value;
            }
        }

    }


    public class ApiResult : ActionSimpResult
    {

        /// <summary>
        /// 请求状态码
        /// </summary>
        public int StatusCode { get; set; }


    }
}
