using System;
using System.Collections.Generic;
using System.Text;
using DbHelper;
using Ordermanager.Dal.Dal.BaseDal;
using Ordermanager.Dal.RedisContext;
using Ordermanager.Model;

namespace Ordermanager.Dal.Dal.CommodityDal
{
    public interface ICommodityDal : IBaseOverAllDal<Commodity>
    {
        IEnumerable<Commodity> GetCommodityByOrderid(string orderId);
    }

    public class CommodityDal : BaseOverAllDal<Commodity>, ICommodityDal
    {
        private readonly IBaseOverAllDal<Commodity> _baseOverAllDal;


        public CommodityDal(IBaseOverAllDal<Commodity> baseOverAllDal) : base(baseOverAllDal)
        {
            _baseOverAllDal = baseOverAllDal;
        }
        public IEnumerable<Commodity> GetCommodityByOrderid(string orderId)
        {
            return _baseOverAllDal.Query<Commodity>("select * from Commodity where _Order=@orderId", new { orderId });
        }

    }
}
