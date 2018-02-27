using WebApp.Models.Database;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using WebApp.DAL.Helpers;

namespace WebApp.DAL
{
    /// <summary>
    /// 数据库连接扩展类
    /// </summary>
    public static class DbContextExtensions
    {       
        /// <summary>
        /// 根据sql获取数字
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static async Task<int> QueryNumberBySqlAsync(this AppDbContext context, string sql, params SqlParameter[] paras)
        {
            var conn = await GetAndOpenConnectionAsync(context);
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(ConvertToMySqlParameters(paras));
            var result = cmd.ExecuteScalar();
            var count = result == null ? 0 : Convert.ToInt32(result);

            return count;
        }

        /// <summary>
        /// 根据sql获取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static async Task<List<T>> QueryListBySqlAsync<T>(this AppDbContext context, string sql, params SqlParameter[] paras) where T : class, new()
        {
            var list = new List<T>();
            using (var reader = await GetListDbDataReaderAsync(context, sql, paras))
            {
                var t = typeof(T);
                var properies = EntityHelper.GetModelProperties(t);
                var cols = GetDataReaderColumns(reader);
                while (reader.Read())
                {
                    var model = new T();
                    foreach (var p in properies)
                    {
                        if (cols.Contains(p.Name))
                        {
                            var v = reader[p.Name];
                            if (v is DBNull)
                                p.SetValue(model, null);
                            else
                                p.SetValue(model, v);
                        }
                    }
                    list.Add(model);
                }
                reader.Close();
            }

            return list;
        }

        /// <summary>
        /// 获取动态对象列表根据sql
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static async Task<List<dynamic>> QueryDynamicListBySqlAsync(this AppDbContext context, string sql, params SqlParameter[] paras)
        {
            var list = new List<dynamic>();
            using (var reader = await GetListDbDataReaderAsync(context, sql, paras))
            {
                var cols = GetDataReaderColumns(reader);
                while (reader.Read())
                {
                    dynamic model = new ExpandoObject();
                    var dic = (IDictionary<string, object>)model;
                    foreach (var col in cols)
                    {
                        dic.Add(col, reader[col]);
                    }
                    list.Add(model);
                }
                reader.Close();
            }

            return list;
        }

        /// <summary>
        /// 获取reader结果集的字段列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static List<string> GetDataReaderColumns(DbDataReader reader)
        {
            var list = new List<string>();
            var dt = reader.GetSchemaTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(dt.Rows[i]["ColumnName"].ToString());
            }

            return list;
        }

        /// <summary>
        /// 获取列表的sqlreader
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static async Task<DbDataReader> GetListDbDataReaderAsync(AppDbContext context, string sql, params SqlParameter[] paras)
        {
            var conn = await GetAndOpenConnectionAsync(context);
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (paras.Length > 0)
                cmd.Parameters.AddRange(ConvertToMySqlParameters(paras));

            return cmd.ExecuteReader();
        }

        /// <summary>
        /// 获取并打开数据库连接
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static async Task<DbConnection> GetAndOpenConnectionAsync(AppDbContext context)
        {
            var conn = context.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            return conn;
        }

        /// <summary>
        /// 转换为mysql参数
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static MySqlParameter[] ConvertToMySqlParameters(SqlParameter[] paras)
        {
            var list = new List<MySqlParameter>();
            foreach (var para in paras)
            {
                list.Add(new MySqlParameter(para.ParameterName, para.Value));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQueryAsync(this AppDbContext context, string sql, params SqlParameter[] paras)
        {
            var conn = await GetAndOpenConnectionAsync(context);
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (paras.Length > 0)
                cmd.Parameters.AddRange(ConvertToMySqlParameters(paras));
            var result = await cmd.ExecuteNonQueryAsync();

            return result;
        }
    }
}
