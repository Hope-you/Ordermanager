﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbHelper;
using Microsoft.AspNetCore.Authorization;
using Ordermanager.Bll;
using Ordermanager.Dal;
using Ordermanager.Model;

namespace Ordermanager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Bll.UserBll _userBll;
        //private readonly UserDal _users;
        //public LoginController(UserDal users)
        //{
        //    _users = users;
        //}
        public LoginController(Bll.UserBll userBll)
        {
            _userBll = userBll;
        }

        [HttpGet("{userName}/{userPwd}")]
        public Model.User Get(string userName, string userPwd)
        {
            return _userBll.GetUserByLogin(userName, userPwd);
        }

        /// <summary>
        /// 获取所有的User数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Model.User> GetAll()
        {
            return _userBll.GetAll();
        }


        [HttpPost]
        public string Authenticated([FromBody] LoginRequestBody loginRequestBody)
        {
            string token;
            if (_userBll.UserLogin(loginRequestBody, out token))
                return token;
            return null;
        }
    }
}