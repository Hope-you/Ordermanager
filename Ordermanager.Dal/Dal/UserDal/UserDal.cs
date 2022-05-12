using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using DbHelper;
using Microsoft.Extensions.DependencyInjection;
using Ordermanager.Dal.RedisContext;
using Ordermanager.Model;
using StackExchange.Redis;

namespace Ordermanager.Dal
{


    /// <summary>
    /// 这个接口继承了Idal<User> 用来扩展Userdal的接口，
    /// 因为不是所有实体的接口都是一样的，只是把一样的给提取出啦
    /// </summary>
    public interface IUserDal : IDal<User>
    {
        public User GetUserByLogin(string userName, string userPwd);

    }

    /// <summary>
    /// 继承了类BaseDal中的基本方法，接口IUserDal进行扩展的方法
    /// </summary>
    public class UserDal : BaseDal<User>, IUserDal
    {
        private readonly IDapperExtHelper<User> _dapperExtHelper;
        private readonly IRedisHelper<User> _redisHelper;

        public UserDal(IDapperExtHelper<User> dapperExtHelper, IRedisHelper<User> redisHelper) : base(dapperExtHelper, redisHelper)
        {
            _dapperExtHelper = dapperExtHelper;
            _redisHelper = redisHelper;
        }
        public User GetUserByLogin(string userName, string userPwd)
        {
            string sql = "select * from User where userName=@userName and userPwd=@userPwd";
            //var aaa = _db.QueryFirst<User>(sql, new {userName, userPwd});
            return _dapperExtHelper.QueryFirst<User>(sql, new { userName, userPwd });
        }

    }
}
