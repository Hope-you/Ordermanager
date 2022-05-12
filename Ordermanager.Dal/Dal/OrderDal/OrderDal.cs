using System;
using System.Collections.Generic;
using System.Text;
using DbHelper;
using Ordermanager.Dal.RedisContext;
using Ordermanager.Model;

namespace Ordermanager.Dal.OrderDal
{


    public interface IOrderDal : IDal<Order>
    {
        /// <summary>
        /// 根据酒店的id获取所有订单
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        IEnumerable<Order> GetOrdersByHotelId(string hotelId);
    }
    public class OrderDal : BaseDal<Order>, IOrderDal
    {
        private readonly IDapperExtHelper<Order> _dapperExtHelper;
        private readonly IRedisHelper<Order> _redisHelper;

        public OrderDal(IDapperExtHelper<Order> dapperExtHelper,IRedisHelper<Order> redisHelper) : base(dapperExtHelper, redisHelper)
        {
            _dapperExtHelper = dapperExtHelper;
            _redisHelper = redisHelper;
        }

        public IEnumerable<Order> GetOrdersByHotelId(string hotelId)
        {
            return _dapperExtHelper.Query<Order>("SELECT * FROM Orders where _Hotel=@hotelId", new {hotelId});
        }
    }
}
