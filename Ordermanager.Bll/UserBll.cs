using System;
using DbHelper;
using Ordermanager.Dal;
using Ordermanager.Model;

namespace Ordermanager.Bll
{
    //[Service]
    public class UserBll
    {
        private UserDal _userDal;

        private readonly IDapperExtHelper<User> _dapperExtHelper;

        public UserBll(IDapperExtHelper<User> dapperExtHelper, UserDal userDal)
        {
            _dapperExtHelper = dapperExtHelper;
            _userDal = userDal;
        }

        public User GetUserByLogin(string userName, string userPwd)
        {
            var user = _userDal.GetUserByLogin(userName, userPwd);
            user.userLoginTime = DateTime.Now;
            if (user.IsDelete)
                return default;
            return user;
        }



    }
}
