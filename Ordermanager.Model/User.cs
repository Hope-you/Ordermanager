using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Ordermanager.Model
{
    /// <summary>
    /// dapper的扩展方法getAll会自动加上s 这里指定表名
    /// </summary>
    [Table("User")]
    public class User : BaseModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string userPwd { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime userRegTime { get; set; }

        /// <summary>
        /// 上次登陆时间
        /// </summary>
        public DateTime userLoginTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
