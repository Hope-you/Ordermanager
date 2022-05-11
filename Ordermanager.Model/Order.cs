using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Ordermanager.Model
{
    [Table("Orders")]
    public class Order : BaseModel
    {
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 完成  未完成
        /// </summary>
        public bool Status { get; set; }

        public string _Hotel { get; set; }
        /// <summary>
        /// 记录创建时间  不是下单时间，下单时间可以更改的
        /// </summary>
        public DateTime CreateDate { get; set; }
    }

    /// <summary>
    /// 订单中商品的状态  
    /// </summary>
    public enum GoodsStatus : byte
    {
        //      发货 开票  收款  
        //  0    1     1    1

        未发货未开票未收款 = 0b000,
        未发货未开票已收款 = 0b001,
        未发货已开票未收款 = 0b010,
        已发货未开票未收款 = 0b100,
        完成 = 0b111, //0111
        已发货已开票未收款 = 0b110,
        已发货未开票已收款 = 0b101,
        未发货已开票已收款 = 0b011
    }
}
