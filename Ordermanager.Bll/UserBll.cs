using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DbHelper;
using Ordermanager.Dal;
using Ordermanager.Model;

namespace Ordermanager.Bll
{
    //[Service]
    public class UserBll
    {
        private IUserDal _userDal;
        private ApiResult _apiResult;

        /// <summary>
        /// 从容器里获取数据dal层
        /// </summary>
        /// <param name="userDal"></param>
        /// <param name="apiResult">规范返回结果</param>
        public UserBll(IUserDal userDal, ApiResult apiResult)
        {
            _userDal = userDal;
            _apiResult = apiResult;
        }

        public ApiResult GetAll()
        {
            var allUser = _userDal.SelectAll();
            _apiResult.StatusCode = 200;
            if (allUser != null && allUser.Any())
            {
                _apiResult.Data = allUser;
                _apiResult.Msg = "请求成功";
                _apiResult.Success = true;
            }
            else
            {
                _apiResult.Data = new List<User>();
                _apiResult.Msg = "请求失败";
                _apiResult.Success = false;
            }
            return _apiResult;
        }

        public ApiResult UserLogin(LoginRequestBody loginRequestBody)
        {
            string token;
            loginRequestBody.passWord = CreateMd5(loginRequestBody.passWord);
            var isAuth = _userDal.IsAuthenticated(loginRequestBody, out token);
            _apiResult.Data = token;
            _apiResult.Msg = isAuth ? "登陆成功" : "登陆失败";
            _apiResult.Success = isAuth;
            return _apiResult;
        }

        public ApiResult RegUser(LoginRequestBody loginRequestBody)
        {
            //需要注册的用户信息
            var regUser = new User
            {
                id = Guid.NewGuid().ToString("N"),
                IsDelete = true,
                userPwd = CreateMd5(loginRequestBody.passWord),
                userRegTime = DateTime.Now,
                userName = loginRequestBody.userName
            };
            _apiResult.Success = _userDal.RegUser(regUser);
            _apiResult.Msg = _apiResult.Success ? "注册成功" : "注册失败";
            _apiResult.Data = _apiResult.Success;
            _apiResult.StatusCode = 200;
            return _apiResult;
        }

        private string CreateMd5(string str)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str)).Select(o => o.ToString("X")));
        }
    }
}
