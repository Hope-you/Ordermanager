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
    [ResultFilter]
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

        [HttpGet("{userName}/{userPwd}")]
        public ActionSimpResult Get(string userName, string userPwd)
        {
            return _userBll.GetUserByLogin(userName, userPwd);
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
        public ActionSimpResult Authenticated([FromBody] LoginRequestBody loginRequestBody)
        {
            var data = _userBll.UserLogin(loginRequestBody);
            return data;
        }
    }
}