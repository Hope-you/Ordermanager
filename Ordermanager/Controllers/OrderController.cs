using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ordermanager.Bll;
using Ordermanager.Model;

namespace Ordermanager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderBll _order;

        public OrderController(OrderBll order)
        {
            _order = order;
        }

        [HttpGet]
        public IEnumerable<Order> getOrders(string hotelId)
        {

            return _order.GetOrdersByHotelId(hotelId);
        }

    }
}
