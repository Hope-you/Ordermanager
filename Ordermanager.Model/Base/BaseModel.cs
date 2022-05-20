using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Ordermanager.Model
{
    public class BaseModel
    {
        /// <summary>
        /// 主键id,ExplicitKey为了可以insert
        /// </summary>
        [ExplicitKey]
        public string id { get; set; }
    }
}
