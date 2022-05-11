using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Ordermanager.Model;

namespace DbHelper
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




        public T QueryFirst<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 查询所有的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered">是否使用缓存</param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 执行方法  增删改查之类的
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int Execute(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

    }

    public class DapperExtHelper<T> : IDapperExtHelper<T> where T : BaseModel, new()
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




        /// <summary>
        /// 查询第一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数对象</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间，单位是秒</param>
        /// <param name="commandType">sql种类 语句 存储过程</param>
        /// <returns></returns>
        public T QueryFirst<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                if (DbContext.DbConnection.State != ConnectionState.Open) DbContext.DbConnection.Open();
                using (transaction = DbContext.DbConnection.BeginTransaction())
                {
                    var obj = DbContext.DbConnection.QueryFirst<T>(sql, param, transaction, commandTimeout, commandType);
                    if (obj == null)
                        throw new Exception("没有查询到数据");
                    transaction.Commit();
                    DbContext.DbConnection.Close();
                    return obj;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        /// <summary>
        /// 查询所有的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered">是否使用缓存</param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbContext.DbConnection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// 执行方法  增删改查之类的
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int Execute(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbContext.DbConnection.Execute(sql, param, transaction, commandTimeout, commandType);
        }

    }
}
