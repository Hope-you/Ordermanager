using System;
using System.Collections.Generic;
using System.Text;
using DbHelper;
using Newtonsoft.Json;
using Ordermanager.Dal.Dal.BaseDal;
using Ordermanager.Dal.RedisContext;
using Ordermanager.Model;

namespace Ordermanager.Dal.HotelDal
{
    public interface IHotelDal : IBaseOverAllDal<Hotel>
    {
        /// <summary>
        /// 根据Hotel的_User来查询所属的User
        /// </summary>
        /// <param name="foreign"></param>
        /// <returns></returns>
        public IEnumerable<Hotel> GetHotelByUser(string userId);

        public bool insertHotel(Hotel hotel);

    }

    public class HotelDall : BaseOverAllDal<Hotel>, IHotelDal
    {
        private readonly IBaseOverAllDal<Hotel> _baseOverAllDal;

        public HotelDall(IBaseOverAllDal<Hotel> baseOverAllDal) : base(baseOverAllDal)
        {
            _baseOverAllDal = baseOverAllDal;
        }

        /// <summary>
        /// 根据用户的id获取其所有的Hotel
        /// </summary>
        /// <param name="userId">用户的id</param>
        /// <returns></returns>
        public IEnumerable<Hotel> GetHotelByUser(string userId)
        {
            string sql = "select * from Hotel where _user=@userId";
            //优先从redis中获取  redis的key设置成id+业务名字  没有别的好想法
            return _baseOverAllDal.GetObjListFromRedisFirst(sql, new { userId }, userId + "HotelList", true);
        }

        public bool insertHotel(Hotel hotel)
        {
            if (_baseOverAllDal.Insert(hotel) <= 0) return false;
            return true;
        }

    }
}
