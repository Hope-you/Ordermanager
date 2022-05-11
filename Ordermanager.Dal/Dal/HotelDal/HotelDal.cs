using System;
using System.Collections.Generic;
using System.Text;
using DbHelper;
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
        public HotelDall(IDapperExtHelper<Hotel> dapperExtHelper) : base(dapperExtHelper)
        {
            _dapperExtHelper = dapperExtHelper;
        }

        /// <summary>
        /// 根据用户的id获取其所有的Hotel
        /// </summary>
        /// <param name="userId">用户的id</param>
        /// <returns></returns>
        public IEnumerable<Hotel> GetHotelByUser(string userId)
        {
            return _dapperExtHelper.Query<Hotel>("select * from Hotel where _user=@userId", new { userId });
        }


    }
}
