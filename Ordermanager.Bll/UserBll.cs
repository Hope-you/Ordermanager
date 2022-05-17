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

        /// <summary>
        /// 从容器里获取数据dal层
        /// </summary>
        /// <param name="userDal"></param>
        public UserBll(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public User GetUserByLogin(string userName, string userPwd)
        {
            var user = _userDal.GetUserByLogin(userName, userPwd);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _userDal.SelectAll();
        }

        public bool UserLogin(LoginRequestBody loginRequestBody, out string token)
        {
            return _userDal.IsAuthenticated(loginRequestBody, out token);
        }

        public bool RegUser(LoginRequestBody loginRequestBody)
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
            return _userDal.RegUser(regUser);
        }
    }
}
