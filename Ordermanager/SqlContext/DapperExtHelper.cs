using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Ordermanager.Model;

namespace Ordermanager.SqlContext
{
    public interface IDapperExtHelper<T> where T : BaseModel, new()
    {
        /// <summary>
        /// 根据主键获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(string id);

        /// <summary>
        /// 获取当前实体的所有值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// 插入一个实体，
        /// </summary>
        /// <param name="t">需要插入的实体</param>
        /// <returns></returns>
        long Insert(T t);

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Delete(T t);

        /// <summary>
        /// 删除所有的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeletAll(string id);

        /// <summary>
        /// 更新一个实体  tips dapper和ef也都是需要先查询出来然后再传递实体进行更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update(T t);
    }

    public class DapperExtHelper<T> : IDapperExtHelper<T> where T:BaseModel,new()
    {
        /// <summary>
        /// 根据主键获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(string id)
        {
            return DbContext.DbConnection.Get<T>(id);
        }

        /// <summary>
        /// 获取当前实体的所有值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return DbContext.DbConnection.GetAll<T>();
        }

        /// <summary>
        /// 插入一个实体，
        /// </summary>
        /// <param name="t">需要插入的实体</param>
        /// <returns></returns>
        public long Insert(T t)
        {
            return DbContext.DbConnection.Insert(t);
        }

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Delete(T t)
        {
            return DbContext.DbConnection.Delete(t);
        }

        /// <summary>
        /// 删除所有的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeletAll(string id)
        {
            return DbContext.DbConnection.DeleteAll<T>();
        }

        /// <summary>
        /// 更新一个实体  tips dapper和ef也都是需要先查询出来然后再传递实体进行更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update(T t)
        {
            return DbContext.DbConnection.Update(t);
        }


    }
}
