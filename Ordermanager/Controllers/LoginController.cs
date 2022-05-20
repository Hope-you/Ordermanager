using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbHelper;
using Microsoft.AspNetCore.Authorization;
using Ordermanager.Api.Controllers.BaseController;
using Ordermanager.Bll;
using Ordermanager.Dal;
using Ordermanager.Model;
using Ordermanager.Dal.Filter;

namespace Ordermanager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : PackageControllerBase
    {
        private readonly UserBll _userBll;
        //private readonly UserDal _users;
        //public LoginController(UserDal users)
        //{
        //    _users = users;
        //}
        public LoginController(UserBll userBll)
        {
            _userBll = userBll;
        }
        

        /// <summary>
        /// 获取所有的User数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_userBll.GetAll());
        }


        [HttpPost]
        public ApiResult Authenticated([FromBody] LoginRequestBody loginRequestBody)
        {
            return _userBll.UserLogin(loginRequestBody);
        }
    }
}