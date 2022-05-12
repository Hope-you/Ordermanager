using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Ordermanager.Model.Redis;
using StackExchange.Redis;

namespace Ordermanager.Dal.RedisContext
{
    public static class redisContext
    {
        public static IDatabase redisDb;
        private static ConnectionMultiplexer _conn;

        public static void AddRedisContext(this IServiceCollection services, RedisConection rediConection)
        {
            _conn = ConnectionMultiplexer.Connect(rediConection.Connection);
            redisDb = _conn.GetDatabase(rediConection.DefaultDb);
            //把数据库注入单例
            //redis可以有很多个数据库
            //这里只注入一个
            services.AddSingleton(redisDb);
            //注入helper类
            services.AddScoped(typeof(IRedisHelper<>), typeof(RedisHelper<>));
        }
    }

}
