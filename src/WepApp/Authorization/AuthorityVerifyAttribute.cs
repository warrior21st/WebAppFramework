using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

#pragma warning disable 1591
namespace WebApp.Authorization
{
    /// <summary>
    /// 权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorityVerifyAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _interfaceName;

        private readonly string _operationName;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="interfaceName">接口名称</param>
        /// <param name="operationName">操作名称</param>
        public AuthorityVerifyAttribute(string interfaceName,string operationName)
        {
            _interfaceName = interfaceName;
            _operationName = operationName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.SetRequestInterfaceName(_interfaceName);
            context.HttpContext.SetRequestInterfaceNeedOperation(_operationName);
        }
    }
}
