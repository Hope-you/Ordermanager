using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ordermanager.Bll;

namespace Ordermanager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommodityController : ControllerBase
    {
        private readonly CommodityBll _commodity;

        public CommodityController(CommodityBll commodity)
        {
            _commodity = commodity;
        }

        [HttpGet]
        public IEnumerable<Model.Commodity> GetCommoditiesByOrderID(string orderId)
        {
            return _commodity.GetCommoditiesByOrderId(orderId);
        }
    }
}
