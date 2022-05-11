using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbHelper;
using Ordermanager.Bll;
using Ordermanager.Dal.HotelDal;
using Ordermanager.Model;

namespace Ordermanager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly HotelBll _hotelBll;
        public HotelController(HotelBll hotelDal)
        {
            _hotelBll = hotelDal;
        }

        [HttpGet]
        public IEnumerable<Hotel> GetHotelByUser(string userId)
        {
            return _hotelBll.GetHotelByUser(userId);
        }
    }
}
