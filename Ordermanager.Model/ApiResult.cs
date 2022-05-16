using System;
using System.Collections.Generic;
using System.Text;

namespace Ordermanager.Model
{
    public class ApiResult
    {
        /// <summary>
        /// 请求状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 是否是成功的
        /// </summary>
        public bool Success
        {
            get { return this.StatusCode == 200; }
        }

        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 数据实体
        /// </summary>
        public object Data { get; set; }

    }
}
