using System;
using System.Collections.Generic;
using System.Text;
using Ordermanager.Dal.Dal.CommodityDal;

namespace Ordermanager.Bll
{
    /// <summary>
    /// bll可以再提取一个接口，控制器中直接获取接口就行了
    /// </summary>
    public class CommodityBll
    {
        private readonly ICommodityDal _commodityDal;

        public CommodityBll(ICommodityDal commodityDal)
        {
            _commodityDal = commodityDal;
        }

        public IEnumerable<Model.Commodity> GetCommoditiesByOrderId(string orderId)
        {
            return _commodityDal.GetCommodityByOrderid(orderId);
        }
    }
}
