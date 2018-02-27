using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// 查询扩展类
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// 添加排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="columnName"></param>
        /// <param name="order"></param>
        public static IQueryable<T> AddOrderBy<T>(this IQueryable<T> query, string columnName, string order) where T : class
        {
            ParameterExpression param = Expression.Parameter(typeof(T), columnName);
            Expression body = param;

            if (Nullable.GetUnderlyingType(body.Type) != null)
                body = Expression.Property(body, "Value");
            var t = typeof(T);
            PropertyInfo sortProperty = t.GetProperty(columnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (sortProperty == null)
                throw new Exception("对像上不存在" + columnName + "的字段");

            body = Expression.MakeMemberAccess(body, sortProperty);
            LambdaExpression keySelectorLambda = Expression.Lambda(body, param);
            string queryMethod = order.CompareIgnoreCase("DESC") ? "OrderByDescending" : "OrderBy";
            query = query.Provider.CreateQuery<T>(Expression.Call(typeof(Queryable), queryMethod,
                                                               new Type[] { t, body.Type },
                                                               query.Expression,
                                                               Expression.Quote(keySelectorLambda)));

            return query;
        }
    }
}
