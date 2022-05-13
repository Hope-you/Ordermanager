using System;
using System.Collections.Generic;
using System.Text;
using DbHelper;
using Ordermanager.Dal.Dal.BaseDal;
using Ordermanager.Dal.RedisContext;
using Ordermanager.Model;

namespace Ordermanager.Dal.OrderDal
{


    public interface IOrderDal : IBaseOverAllDal<Order>
    {
        /// <summary>
        /// 根据酒店的id获取所有订单
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        IEnumerable<Order> GetOrdersByHotelId(string hotelId);
    }
    public class OrderDal : BaseOverAllDal<Order>, IOrderDal
    {
        private readonly IBaseOverAllDal<Order> _baseOverAllDal;

        public OrderDal(IBaseOverAllDal<Order> baseOverAllDal) : base(baseOverAllDal)
        {
            _baseOverAllDal = baseOverAllDal;
        }

        public IEnumerable<Order> GetOrdersByHotelId(string hotelId)
        {
            return _baseOverAllDal.Query<Order>("SELECT * FROM Orders where _Hotel=@hotelId", new { hotelId });
        }

    }
}
