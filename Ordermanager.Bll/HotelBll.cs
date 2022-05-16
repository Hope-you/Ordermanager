using System;
using System.Collections.Generic;
using System.Text;
using Ordermanager.Dal.HotelDal;

namespace Ordermanager.Bll
{
    public class HotelBll
    {
        private readonly IHotelDal _hotelDal;
        public HotelBll(IHotelDal hotelDal)
        {
            this._hotelDal = hotelDal;
        }

        public IEnumerable<Model.Hotel> GetHotelByUser(string userId)
        {
            return _hotelDal.GetHotelByUser(userId);
        }
    }
}
