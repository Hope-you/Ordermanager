using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Ordermanager.Model
{
    [Table("Commodity")]
    public class Commodity :BaseModel
    {
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品的成交单格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Counts { get; set; }

        /// <summary>
        /// 商品的原价，
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// 所属的订单外键
        /// </summary>
        public string _Order { get; set; }
    }
}
