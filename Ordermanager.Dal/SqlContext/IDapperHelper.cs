using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbHelper
{
    public interface IDapperHelper
    {
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



        /// <summary>
        /// 根据主键获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(string id);

        /// <summary>
        /// 获取当前实体的所有值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// 插入一个实体，
        /// </summary>
        /// <param name="t">需要插入的实体</param>
        /// <returns></returns>
        long Insert<T>(T t);

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Delete<T>(T t);

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
        bool Update<T>(T t);
    }
}
