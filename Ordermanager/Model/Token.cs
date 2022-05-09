using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordermanager.Model
{
    public class Token
    {
        public string TokenContext { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
