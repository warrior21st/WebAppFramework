using WebApp.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.Database;
using CommonHelpers.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApp.Models.Database.AspNet;
using Microsoft.AspNetCore.Identity;

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

        /// <summary>
        /// 管理用户是否已登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool ManageIsLogin(this HttpContext context)
        {
            return !string.IsNullOrWhiteSpace(context.Session.GetString(USER_LOGIN_SESSION_NAME));
        }

        /// <summary>
        /// 管理用户登入
        /// </summary>
        /// <param name="context"></param>
        /// <param name="uid"></param>
        /// <param name="userName"></param>
        public static void ManageSignIn(this HttpContext context, int uid, string userName)
        {
            context.SignInBySession(USER_LOGIN_SESSION_NAME, uid, userName);
        }

        /// <summary>
        /// 管理用户登出
        /// </summary>
        /// <param name="context"></param>
        public static void ManageSignOut(this HttpContext context)
        {
            context.SignOutBySession(USER_LOGIN_SESSION_NAME);
        }

        /// <summary>
        /// 获取管理用户
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static AspNetUser GetManageLoginUser(this HttpContext context)
        {
            AspNetUser user = null;
            if (context.AppIsLogin())
            {
                var infoStr = DataHelper.UnProtect(context.Session.GetString(USER_LOGIN_SESSION_NAME));
                var arr = infoStr.Split(Environment.NewLine.ToArray());
                user = new AspNetUser();
                user.Id = int.Parse(arr[0]);
                user.UserName = arr.Length > 1 ? arr[1] : null;
            }

            return user;
        }

        /// <summary>
        /// 业务用户是否已登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool AppIsLogin(this HttpContext context)
        {
            return context.User != null && context.User.Identity != null && !string.IsNullOrWhiteSpace(context.User.Identity.Name);
        }

        /// <summary>
        /// 业务登入
        /// </summary>
        /// <param name="context"></param>
        /// <param name="uid"></param>
        /// <param name="aliasName"></param>
        /// <param name="isPersistent"></param>
        public static void AppSignIn(this HttpContext context, int uid, string aliasName, bool isPersistent)
        {
            Task.WaitAll(context.SignInByCookieAsync(uid, aliasName, isPersistent));
        }

        /// <summary>
        /// 业务登出
        /// </summary>
        /// <param name="context"></param>
        public static void AppSignOut(this HttpContext context)
        {
            if (context.AppIsLogin())
                Task.WaitAll(context.SignOutByCookieAsync());
        }

        /// <summary>
        /// 获取业务用户
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static AppUser GetAppLoginUser(this HttpContext context)
        {
            AppUser user = null;
            if (context.AppIsLogin())
            {
                var infoStr = DataHelper.UnProtect(context.User.Identity.Name);
                var arr = infoStr.Split(Environment.NewLine.ToArray());
                user = new AppUser();
                user.Id = int.Parse(arr[0]);
                user.AliasName = arr.Length > 1 ? arr[1] : null;
            }

            return user;
        }

        /// <summary>
        /// 获取用户信息字符串
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        private static string GetUserInfoString(int uid, string aliasName)
        {
            var str = uid.ToString();
            str += !string.IsNullOrWhiteSpace(aliasName) ? $"{Environment.NewLine}{aliasName}" : "";

            return DataHelper.Protect(str);
        }

        #region cookie登入登出

        private static async Task SignInByCookieAsync(this HttpContext context, int uid, string aliasName, bool isPersistent)
        {
            var info = GetUserInfoString(uid, aliasName);
            var id = new ClaimsIdentity("authenticationType", "nameType", "roleType");
            id.AddClaim(new Claim("idType", uid.ToString()));
            id.AddClaim(new Claim("nameType", info));
            var cp = new ClaimsPrincipal(id);
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, cp, new AuthenticationProperties() { IsPersistent = isPersistent });
        }

        private static async Task SignOutByCookieAsync(this HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await context.SignOutAsync(IdentityConstants.ApplicationScheme);
            await context.SignOutAsync(IdentityConstants.ExternalScheme);
            await context.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);
        }

        #endregion

        #region session登入登出

        private static void SignInBySession(this HttpContext context, string sessionName, int uid, string userName)
        {
            context.Session.SetString(sessionName, GetUserInfoString(uid, userName));
        }

        private static void SignOutBySession(this HttpContext context, string sessionName)
        {
            if (!string.IsNullOrWhiteSpace(context.Session.GetString(sessionName)))
                context.Session.Remove(sessionName);
        }

        #endregion
    }
}
