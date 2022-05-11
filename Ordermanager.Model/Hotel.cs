using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Ordermanager.Model
{
    [Table("Hotel")]
    public class Hotel : BaseModel
    {
        public string HotelName { get; set; }
        public string HotelPosition { get; set; }
        public string Tel { get; set; }
        public string Client { get; set; }
        public bool IsDelete { get; set; }
        /// <summary>
        /// 用户的外键
        /// </summary>
        public string _user { get; set; }

    }
}
