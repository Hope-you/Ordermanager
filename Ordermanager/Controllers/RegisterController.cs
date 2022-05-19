using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ordermanager.Bll;
using Ordermanager.Model;

namespace Ordermanager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserBll _userBll;

        public RegisterController(UserBll userBll)
        {
            _userBll = userBll;
        }
        [HttpPost]
        public ActionSimpResult UserReg(LoginRequestBody loginRequestBody)
        {
            var data = _userBll.RegUser(loginRequestBody);
            data.Msg = (bool)data.Data ? "注册成功" : "注册失败";
            return data;
        }
    }
}
