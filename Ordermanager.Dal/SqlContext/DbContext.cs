using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace Ordermanager.SqlContext
{
    public static class DbContext
    {
        private static IDbConnection _dbConnection = new MySqlConnection();

        public static IDbConnection DbConnection
        {
            get
            {
                if (string.IsNullOrEmpty(_dbConnection.ConnectionString))
                {
                    _dbConnection.ConnectionString = connectionString;
                }
                return _dbConnection;
            }
        }

        private static string _connectionString;

        public static string connectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        /// <summary>
        /// 扩展方法，把这个数据库上下文注入到容器中,并且在注入的时候配置连接字符串
        /// </summary>
        /// <param name="service">IServiceCollection对象</param>
        /// <param name="connectString">需要配置的连接字符串</param>
        public static void AddDbcontext(this IServiceCollection service,string connectString)
        {
            connectionString = connectString;
            service.AddScoped<IDapperHelper, DapperHelper>();
        }
    }
}
