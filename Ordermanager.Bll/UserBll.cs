using System;
using System.Collections.Generic;
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
            return _userDal.selectAll();
        }

        public bool UserLogin(LoginRequestBody loginRequestBody, out string token)
        {
            return _userDal.IsAuthenticated(loginRequestBody, out token);
        }

    }
}
