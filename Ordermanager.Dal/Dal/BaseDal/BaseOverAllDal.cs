using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DbHelper;
using Newtonsoft.Json;
using Ordermanager.Dal.RedisContext;
using Ordermanager.Model;

namespace Ordermanager.Dal.Dal.BaseDal
{

    /// <summary>
    /// 用来操作redis和mysql数据库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseOverAllDal<T> : IDapperExtHelper<T>, IRedisHelper<T> where T : BaseModel, new()
    {
        /// <summary>
        /// 尝试从redis获取，如果获取不到就从Mysql获取并且写到Redis中
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string TryGetAndSet(string key);


        /// <summary>
        /// 根据键查询出写入的对象
        /// 其实也是字符串，反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        T GetEntityByString(string key);

        /// <summary>
        /// 把一个对象写进redis
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="t">写入的对象</param>
        /// <returns></returns>
        bool SetEntityByString(string key, T t);


        /// <summary>
        /// 尝试获取数据，如果没有获取到返回false，
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">如果有数据value就是值</param>
        /// <returns></returns>
        bool TryGetString(string key, out string value);

        /// <summary>
        /// 尝试获取一个实体，
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="t">获取到的实体</param>
        /// <returns>是否成功获取到</returns>
        bool TryGetEntity(string key, out T t);


        /// <summary>
        /// 获取一个对象  优先在redis中获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T TryGetObj(string key);


        /// <summary>
        /// 从mysql获取对象列表
        /// </summary>
        /// <param name="sql">语句</param>
        /// <param name="obj">参数</param>
        /// <param name="key">存入redis的key</param>
        /// <param name="setRedis">是否需要往redis里缓存，默认不缓存</param>
        /// <returns></returns>
        IEnumerable<T> GetObjListFromDapper(string sql, object obj, string key, bool setRedis = false);

        /// <summary>
        /// 从redis获取列表
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        IEnumerable<T> GetObjListFromRedis(string key);

        /// <summary>
        /// 优先从redis获取列表，如果没有再从数据库中获取
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj">sql的参数</param>
        /// <param name="key">redis的键</param>
        /// <param name="setRedis">如果redis没有数据是否缓存到redis中</param>
        /// <returns></returns>
        IEnumerable<T> GetObjListFromRedisFirst(string sql, object obj, string key, bool setRedis = false);

    }
    /// <summary>
    /// 把redis和dapper整合在一块 以DapperExtHelper为基类进行扩展的
    /// 这个类实现了IDapper和IRedis接口并且还增加了别的方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseOverAllDal<T> : DapperExtHelper<T>, IBaseOverAllDal<T> where T : BaseModel, new()
    {
        //有些没有想好，IRedisHelper只需要几个简单的增删就行了，序列化实体什么的可以在整合的时候写
        private readonly IRedisHelper<T> _redisHelper;

        public BaseOverAllDal(IRedisHelper<T> _redisHelper)
        {
            this._redisHelper = _redisHelper;
        }


        /// <summary>
        /// 把string转换成对象
        /// </summary>
        /// <param name="value"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool DeObject(string value, out T t)
        {
            try
            {
                t = JsonConvert.DeserializeObject<T>(value);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                t = default;
                return false;
            }
        }
        /// <summary>
        /// 把对象转换成string
        /// </summary>
        /// <param name="t"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool SeObject(T t, out string value)
        {
            try
            {
                value = JsonConvert.SerializeObject(t);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                value = default;
                return false;
            }
        }

        public string GetFromRedis(string key)
        {
            return _redisHelper.GetFromRedis(key);
        }

        public bool SetByRedis(string key, string value)
        {
            return _redisHelper.SetByRedis(key, value);
        }


        public T GetEntityByString(string key)
        {
            var str = GetFromRedis(key);
            if (string.IsNullOrEmpty(str)) return default;
            return JsonConvert.DeserializeObject<T>(str);
        }

        public bool SetEntityByString(string key, T t)
        {
            var str = JsonConvert.SerializeObject(t);
            return _redisHelper.SetByRedis(key, str);
        }

        public bool TryGetString(string key, out string value)
        {
            value = GetFromRedis(key);
            return !string.IsNullOrEmpty(value);
        }


        public bool TryGetEntity(string key, out T t)
        {
            string value;
            //尝试获取数据，如果有就尝试序列化
            if (TryGetString(key, out value))
            {
                Console.WriteLine("获取数据成功");
                if (DeObject(value, out t))
                    return true;
                {
                    Console.WriteLine("获取数据成功，序列化失败！");
                    return false;
                }
            }
            {
                Console.WriteLine("获取数据失败");
                t = default;
                return false;
            }

        }

        public string TryGetAndSet(string key)
        {
            var tempStr = GetFromRedis(key);
            if (string.IsNullOrEmpty(tempStr))
            {
                if (SeObject(GetEntityByString(key), out tempStr))
                {
                    //往redis写数据
                    SetByRedis(key, tempStr);
                    return tempStr;
                }
                return null;
            }
            return tempStr;
        }

        public T TryGetObj(string key)
        {
            //查询redis
            var objStr = TryGetAndSet(key);

            if (string.IsNullOrEmpty(objStr))
            {
                //没有在sql中查询
                return GetEntityByString(key);
            }
            //redis有，转成对象
            DeObject(objStr, out T t);
            return t;
        }

        public IEnumerable<T> GetObjListFromDapper(string sql, object obj, string key, bool setRedis = false)
        {
            var list = Query<T>(sql, obj);
            if (setRedis) SetByRedis(key, JsonConvert.SerializeObject(list));
            return list;
        }

        public IEnumerable<T> GetObjListFromRedis(string key)
        {
            var objStr = GetFromRedis(key);
            if (string.IsNullOrEmpty(objStr)) return default;
            return JsonConvert.DeserializeObject<IEnumerable<T>>(objStr);
        }

        public IEnumerable<T> GetObjListFromRedisFirst(string sql, object obj, string key, bool setRedis = false)
        {
            var redisList = GetObjListFromRedis(key);
            
            if (redisList != null && redisList.Any()) 
                return redisList;
            return GetObjListFromDapper(sql, obj, key, setRedis);


        }
    }
}
