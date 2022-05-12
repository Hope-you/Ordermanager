using System;
using System.Collections.Generic;
using System.Text;
using DbHelper;
using Newtonsoft.Json;
using Ordermanager.Dal.RedisContext;
using Ordermanager.Model;

namespace Ordermanager.Dal.HotelDal
{
    public interface IHotelDal : IDal<Hotel>
    {
        /// <summary>
        /// 根据Hotel的_User来查询所属的User
        /// </summary>
        /// <param name="foreign"></param>
        /// <returns></returns>
        public IEnumerable<Hotel> GetHotelByUser(string userId);

    }

    public class HotelDall : BaseDal<Hotel>, IHotelDal
    {
        private readonly IDapperExtHelper<Hotel> _dapperExtHelper;
        private readonly IRedisHelper<Hotel> _redisHelper;

        public HotelDall(IDapperExtHelper<Hotel> dapperExtHelper, IRedisHelper<Hotel> redisHelper) : base(dapperExtHelper, redisHelper)
        {
            _dapperExtHelper = dapperExtHelper;
            _redisHelper = redisHelper;
        }

        /// <summary>
        /// 根据用户的id获取其所有的Hotel
        /// </summary>
        /// <param name="userId">用户的id</param>
        /// <returns></returns>
        public IEnumerable<Hotel> GetHotelByUser(string userId)
        {
            //这个方法还需要继续封装
            string a = _redisHelper.Get(userId);
            if (string.IsNullOrEmpty(a))
            {
                //如果redis没有就在数据库里查
                var temp = _dapperExtHelper.Query<Hotel>("select * from Hotel where _user=@userId", new {userId});
                //查完写进去
                _redisHelper.Set(userId, JsonConvert.SerializeObject(temp));
                return temp;
            }
            else
            {
                //直接返回redis的数据
                return  JsonConvert.DeserializeObject<IEnumerable<Hotel>>(a);
            }
            
        }


    }
}
