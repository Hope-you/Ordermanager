using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Ordermanager.Bll;
using Ordermanager.Dal.Dal.CommodityDal;
using Ordermanager.Dal.HotelDal;
using Ordermanager.Dal.OrderDal;
using Ordermanager.Model;

namespace Ordermanager.Api.DependencyInjection
{
    public static class Batch
    {
        public static void BatchInjection(this IServiceCollection services)
        {
            services.AddScoped<IHotelDal, HotelDall>();
            services.AddScoped<HotelBll>();
            services.AddScoped<IOrderDal, OrderDal>();
            services.AddScoped<OrderBll>();
            services.AddScoped<ICommodityDal, CommodityDal>();
            services.AddScoped<CommodityBll>();
        }

    }
}
