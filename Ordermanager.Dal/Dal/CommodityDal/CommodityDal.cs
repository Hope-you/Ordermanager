using System;
using System.Collections.Generic;
using System.Text;
using DbHelper;
using Ordermanager.Dal.RedisContext;
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
        private readonly IRedisHelper<Commodity> _redisHelper;

        public CommodityDal(IDapperExtHelper<Commodity> dapperExtHelper, IRedisHelper<Commodity> redisHelper) : base(dapperExtHelper, redisHelper)
        {
            _dapperExtHelper = dapperExtHelper;
            _redisHelper = redisHelper;
        }

        public IEnumerable<Commodity> GetCommodityByOrderid(string orderId)
        {
            return _dapperExtHelper.Query<Commodity>("select * from Commodity where _Order=@orderId", new { orderId });
        }
    }
}
