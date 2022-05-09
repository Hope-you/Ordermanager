using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Ordermanager.Dal;
using Ordermanager.Model;
using Ordermanager.SqlContext;

namespace Ordermanager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IDapperExtHelper<User> _userDapper;

        private readonly UserDal _users;
        //public LoginController(UserDal users)
        //{
        //    _users = users;
        //}
        public LoginController(IDapperExtHelper<User> userDapper)
        {
            _userDapper = userDapper;
        }
        [HttpGet("{userName}/{userPwd}")]
        public User Get(string userName, string userPwd)
        {
            return _users.GetUserByLogin(userName, userPwd);
        }

        /// <summary>
        /// 获取所有的User数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _userDapper.GetAll();
        }
    }
}