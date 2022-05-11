using System;
using System.Collections.Generic;
using System.Text;
using Ordermanager.Dal.OrderDal;

namespace Ordermanager.Bll
{
    public class OrderBll
    {
        private readonly IOrderDal _orderDal;
        public OrderBll( IOrderDal orderDal)
        {
            this._orderDal = orderDal;
        }

        public IEnumerable<Model.Order> GetOrdersByHotelId(string hotelId)
        {
            return _orderDal.GetOrdersByHotelId(hotelId);
        }
        
    }
}
