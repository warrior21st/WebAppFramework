using WebApp.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.Database;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 权限扩展类
    /// </summary>
    public static class AuthorizationExtensions
    {
        private const string INTERFACE_NAME_KEY = "INTERFACE_NAME_KEY";
        private const string INTERFACE_REQUEST_NEED_OPERATION_KEY = "INTERFACE_REQUEST_NEED_OPERATION_KEY";
        private const string LOG_OPERATION_NAME_KEY = "LOG_OPERATION_NAME_KEY";
        private const string USER_LOGIN_SESSION_NAME = "USER_LOGIN_SESSION";

        /// <summary>
        /// 设置本次请求的接口名
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        public static void SetRequestInterfaceName(this HttpContext context, string name)
        {
            context.Items[INTERFACE_NAME_KEY] = name;
        }

        /// <summary>
        /// 获取本次请求的接口名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRequestInterfaceName(this HttpContext context)
        {
            var o = string.Empty;
            if (context.Items.ContainsKey(INTERFACE_NAME_KEY))
                o = context.Items[INTERFACE_NAME_KEY].ToString();

            return o;
        }

        /// <summary>
        /// 获取访问接口所需操作权限
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRequestInterfaceNeedOperation(this HttpContext context)
        {
            var o = string.Empty;
            if (context.Items.ContainsKey(INTERFACE_REQUEST_NEED_OPERATION_KEY))
                o = context.Items[INTERFACE_REQUEST_NEED_OPERATION_KEY].ToString();

            return o;
        }

        /// <summary>
        /// 设置访问接口所需操作权限
        /// </summary>
        /// <param name="context"></param>
        /// <param name="operationName"></param>
        public static void SetRequestInterfaceNeedOperation(this HttpContext context, string operationName)
        {
            context.Items[INTERFACE_REQUEST_NEED_OPERATION_KEY] = operationName;
        }

        /// <summary>
        /// 获取记录日志的操作名称
        /// </summary>
        /// <param name="context"></param>
        public static string GetLogOperationName(this HttpContext context)
        {
            var o = string.Empty;
            if (context.Items.ContainsKey(LOG_OPERATION_NAME_KEY))
                o = context.Items[LOG_OPERATION_NAME_KEY].ToString();

            return o;
        }

        /// <summary>
        /// 设置记录日志的操作名称
        /// </summary>
        /// <param name="context"></param>
        /// <param name="operationName"></param>
        public static void SetLogOperationName(this HttpContext context, string operationName)
        {
            context.Items[LOG_OPERATION_NAME_KEY] = operationName;
        }

        public static bool AppIsLogin(this HttpContext context)
        {
            return context.Session.TryGetValue(USER_LOGIN_SESSION_NAME, out var bytes);
        }

        public static void AppSignIn(this HttpContext context, AppUser user)
        {
            var infostr = GetUserInfoString(user);
            context.Session.SetString(USER_LOGIN_SESSION_NAME, infostr);
        }

        public static void AppSignOut(this HttpContext context)
        {
            if (context.AppIsLogin())
                context.Session.Remove(USER_LOGIN_SESSION_NAME);
        }

        public static AppUser GetAppLoginUser(this HttpContext context)
        {
            AppUser user = null;
            if (context.AppIsLogin())
                user = GetUserByUserInfoString(context.Session.GetString(USER_LOGIN_SESSION_NAME));

            return user;
        }

        private static string GetUserInfoString(AppUser user)
        {
            var str = user.Id.ToString();
            str += !string.IsNullOrWhiteSpace(user.AliasName) ? $"{Environment.NewLine}{user.AliasName}" : "";
            return str;
        }

        private static AppUser GetUserByUserInfoString(string str)
        {
            var arr = str.Split(Environment.NewLine.ToArray());
            var user = new AppUser();
            user.Id = int.Parse(arr[0]);
            user.AliasName = arr.Length > 1 ? arr[1] : null;

            return user;
        }
    }
}
