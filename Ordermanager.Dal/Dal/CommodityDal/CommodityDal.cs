using System;
using System.Collections.Generic;
using System.Text;
using DbHelper;
using Ordermanager.Model;

namespace Ordermanager.Dal.Dal.CommodityDal
{
    public interface ICommodityDal : IDal<Commodity>
    {
        IEnumerable<Commodity> GetCommodityByOrderid(string orderId);
    }

    public class CommodityDal : BaseDal<Commodity>, ICommodityDal
    {
        private readonly IDapperExtHelper<Commodity> _dapperExtHelper;

        public CommodityDal(IDapperExtHelper<Commodity> dapperExtHelper) : base(dapperExtHelper)
        {
            _dapperExtHelper = dapperExtHelper;
        }

        public IEnumerable<Commodity> GetCommodityByOrderid(string orderId)
        {
            return _dapperExtHelper.Query<Commodity>("select * from Commodity where _Order=@orderId", new { orderId });
        }
    }
}
