using System;
using Ordermanager.SqlContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ordermanager.Model;

namespace Ordermanager.Dal
{
    public class UserDal :BaseDal<User>
    {
        private readonly IDapperHelper _dapperHelper;
        
        public UserDal(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public User GetUserByLogin(string userName, string userPwd)
        {
            string sql = "select * from user where userName=@userName and userPwd=@userPwd";
            //var aaa = _db.QueryFirst<User>(sql, new {userName, userPwd});
            return _dapperHelper.QueryFirst<User>(sql, new { userName, userPwd });
        }
    }
}
