using CommonHelpers.Helpers;
using WebApp.Models.Database.Base;
using WebApp.Models.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApp.DAL.Helpers
{
    /// <summary>
    /// 实体帮助类
    /// </summary>
    public static class EntityHelper
    {
        /// <summary>
        /// 获取实体表名
        /// </summary>
        /// <returns></returns>
        public static string GetTableName<T>() where T : BaseEntity
        {
            var entityInfo = GetEntityInfo<T>();

            return entityInfo.TableName;
        }

        /// <summary>
        /// 检查是否包含字段名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <returns></returns>
        public static bool HasColumn<T>(string column) where T : class
        {
            var result = false;
            if (!string.IsNullOrWhiteSpace(column))
            {
                var props = GetModelProperties(typeof(T));
                result = props.Count(x => x.Name == column) > 0;
            }

            return result;
        }

        /// <summary>
        /// 检查是否包含字段名列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public static bool HasColumns<T>(params string[] columnNames) where T : class
        {
            var props = GetModelProperties(typeof(T));
            var result = columnNames != null && columnNames.Length > 0;
            if (result)
            {
                foreach (var f in columnNames)
                {
                    if (props.Count(x => x.Name == f) == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <returns></returns>
        public static EntityInfo GetEntityInfo<T>() where T : BaseEntity
        {
            var t = typeof(T);
            if (!MemoryCacheHelper.Exists(t.FullName))
            {
                var entityInfo = new EntityInfo();
                var tableName = t.Name;
                var obs = t.GetTypeInfo().GetCustomAttribute<TableAttribute>();
                if (obs != null)
                    tableName = obs.Name;
                var fields = new List<string>();
                var properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var dbProps = new List<PropertyInfo>();
                foreach (var p in properties)
                {
                    if (p.CanWrite && p.CanRead && p.GetCustomAttribute<NotMappedAttribute>() == null)
                    {
                        fields.Add(p.Name);
                        dbProps.Add(p);
                    }
                }

                entityInfo.TableName = tableName;
                entityInfo.Properties = dbProps.ToArray();
                entityInfo.Columns = fields;

                MemoryCacheHelper.SetCache(t.FullName, entityInfo, TimeSpan.FromDays(1));
            }

            return MemoryCacheHelper.GetCache<EntityInfo>(t.FullName);
        }

        /// <summary>
        /// 获取模型属性列表
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetModelProperties(Type t)
        {
            if (!MemoryCacheHelper.Exists(t.FullName))
            {
                var properies = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                var list = new List<PropertyInfo>();
                foreach (var p in properies)
                {
                    if (p.CanWrite && p.GetCustomAttribute<NotMappedAttribute>() == null)
                        list.Add(p);
                }
                MemoryCacheHelper.SetCache(t.FullName, list.ToArray(), TimeSpan.FromDays(1));
            }

            return MemoryCacheHelper.GetCache<PropertyInfo[]>(t.FullName);
        }
    }
}
