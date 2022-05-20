using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ordermanager.Dal.HotelDal;
using Ordermanager.Model;

namespace Ordermanager.Bll
{
    public class HotelBll
    {
        private readonly IHotelDal _hotelDal;
        private readonly ApiResult _apiResult;

        public HotelBll(IHotelDal hotelDal, ApiResult apiResult)
        {
            this._hotelDal = hotelDal;
            _apiResult = apiResult;
            _apiResult.Data = new List<Hotel>();
            _apiResult.Success = false;
            _apiResult.Msg = "获取失败";
            _apiResult.StatusCode = 200;
        }

        public ApiResult GetHotelByUser(string userId)
        {
            var allHotel = _hotelDal.GetHotelByUser(userId);
            if (allHotel != null && allHotel.Any())
            {
                _apiResult.Data = allHotel;
                _apiResult.Msg = "获取酒店列表成功";
                _apiResult.Success = true;
            }
            return _apiResult;
        }

        public ApiResult InsertHotel(Hotel hotel)
        {
            if (_hotelDal.insertHotel(hotel))
            {
                _apiResult.Success = true;
                _apiResult.Data = true;
                _apiResult.Msg = "酒店添加成功";
            }

            return _apiResult;
        }
    }
}
