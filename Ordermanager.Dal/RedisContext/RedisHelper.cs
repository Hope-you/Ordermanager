using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
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
        string Get(string key);

        /// <summary>
        /// 往数据写键值
        /// </summary>
        /// <param name="key">写入的键</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        bool Set(string key, string value);

        /// <summary>
        /// 根据键查询出写入的对象
        /// 其实也是字符串，反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        T GetEntity<T>(string key);

        /// <summary>
        /// 把一个对象写进redis
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool SetEntity(T t);
    }

    public class RedisHelper<T> : IRedisHelper<T>
    {
        private readonly IDatabase _redisDb;

        public RedisHelper( IDatabase redisDb)
        {
            _redisDb = redisDb;
        }
        public string Get(string key)
        {
            return _redisDb.StringGet(key);
        }

        public bool Set(string key, string value)
        {
            return _redisDb.StringSet(key, value);
        }

        public T1 GetEntity<T1>(string key)
        {
            throw new NotImplementedException();
        }

        public bool SetEntity(T t)
        {
            throw new NotImplementedException();
        }
        
    }

}
