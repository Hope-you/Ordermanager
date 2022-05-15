using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Ordermanager.Dal.RedisContext
{
    public interface IRedisHelper<T>
    {
        //string
        /// <summary>
        /// 根据key获取字符串
        /// </summary>
        /// <param name="key">需要查询的键</param>
        /// <returns></returns>
        string GetFromRedis(string key);

        /// <summary>
        /// 往数据写键值
        /// </summary>
        /// <param name="key">写入的键</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        bool SetByRedis(string key, string value);


    }

    public class RedisHelper<T> : IRedisHelper<T>
    {
        private readonly IDatabase _redisDb;

        public RedisHelper(IDatabase redisDb)
        {
            _redisDb = redisDb;
        }
        public string GetFromRedis(string key)
        {
            return _redisDb.StringGet(key);
        }

        public bool SetByRedis(string key, string value)
        {
            return _redisDb.StringSet(key, value);
        }
    }

}
