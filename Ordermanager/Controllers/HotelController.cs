using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Ordermanager.Bll;
using Ordermanager.Dal.HotelDal;
using Ordermanager.Model;

namespace Ordermanager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly HotelBll _hotelBll;

        public HotelController(HotelBll hotelBll)
        {
            _hotelBll = hotelBll;
        }

        [HttpGet]
        [UserIdEqualToken]
        public IEnumerable<Hotel> GetHotelByUser(string userId)
        {
            return _hotelBll.GetHotelByUser(userId);
        }
    }

    /// <summary>
    /// 突发奇想，jwt是无状态的
    ///1.如果只根据token中的用户id来的话，会不会不全面
    ///2.上传token给服务器的时候是不是需要也把当前的用户名一块上传
    ///3.好像有那么一点点的多余，emmmm烂尾了。 
    /// </summary>
    public class UserIdEqualToken : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var parameters = context.ActionDescriptor.Parameters.Where(o => o.Name != string.Empty).Select(o => o.Name);

            if (!parameters.Any())
                return;
            return;
            ////这样不具有普适性，应该有办法获取到userId的
            //var aaa = context.HttpContext.Request.Query[parameters.First()];
        }
    }
}
