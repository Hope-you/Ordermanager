using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using DbHelper;
using Ordermanager.Model;

namespace Ordermanager.Dal
{
    //[Service]
    public class UserDal : BaseDal<User>
    {
        private readonly IDapperExtHelper<User> _dapperExtHelper;
        public UserDal(DapperExtHelper<User> dapperExtHelper)
        {
            _dapperExtHelper = dapperExtHelper;
        }
        public User GetUserByLogin(string userName, string userPwd)
        {
            string sql = "select * from user where userName=@userName and userPwd=@userPwd";
            //var aaa = _db.QueryFirst<User>(sql, new {userName, userPwd});
            return _dapperExtHelper.QueryFirst<User>(sql, new { userName, userPwd });
        }
    }
}
