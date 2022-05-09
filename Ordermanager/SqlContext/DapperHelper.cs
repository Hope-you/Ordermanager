using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace Ordermanager.SqlContext
{
    public class DapperHelper : IDapperHelper
    {

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
                if (DbContext.DbConnection.State!=ConnectionState.Open) DbContext.DbConnection.Open();
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
