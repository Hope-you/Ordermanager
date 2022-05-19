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
        private readonly ActionSimpResult _actionSimpResult;

        /// <summary>
        /// 从容器里获取数据dal层
        /// </summary>
        /// <param name="userDal"></param>
        public UserBll(IUserDal userDal, ActionSimpResult actionSimpResult)
        {
            _userDal = userDal;
            _actionSimpResult = actionSimpResult;
        }

        public ActionSimpResult GetUserByLogin(string userName, string userPwd)
        {
            var user = _userDal.GetUserByLogin(userName, userPwd);
            var actionResult = new ActionSimpResult { Data = user };
            return actionResult;
        }

        public IEnumerable<User> GetAll()
        {
            return _userDal.SelectAll();
        }

        public ActionSimpResult UserLogin(LoginRequestBody loginRequestBody)
        {
            string token;
            var isAuth = _userDal.IsAuthenticated(loginRequestBody, out token);
            //如果对Msg和Success设置值的话，需要先赋值data，不然会被刷新，详细信息看ActionSimpResult这个类
            _actionSimpResult.Data = token;
            _actionSimpResult.Success = isAuth;
            return _actionSimpResult;
        }

        public ActionSimpResult RegUser(LoginRequestBody loginRequestBody)
        {
            var regUser = new User
            {
                id = Guid.NewGuid().ToString("N"),
                IsDelete = true,
                userPwd = string.Join("", MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(loginRequestBody.userName))
                    .Select(o => o.ToString("X"))),
                userRegTime = DateTime.Now,
                userName = loginRequestBody.userName
            };
            _actionSimpResult.Data = _userDal.RegUser(regUser);

            return _actionSimpResult;
        }
    }
}
