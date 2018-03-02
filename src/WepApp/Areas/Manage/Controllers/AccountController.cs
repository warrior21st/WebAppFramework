using CommonHelpers.Helpers;
using WebApp.Helpers;
using WebApp.Models.Database;
using WebApp.Models.Database.AspNet;
using GoogleAuthenticator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OtpSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using CommonHelpers.Algorithm;
using WebApp.DAL;

namespace WebApp.Areas.Manage.Controllers
{
    /// <summary>
    /// 账号控制器
    /// </summary>
    [Area("Manage")]
    public class AccountController : ManageBaseController
    {
        public AccountController(ILogger<AccountController> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {

        }

        /// <summary>
        /// 登陆页面
        /// </summary>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl = null)
        {
            return View();
        }

        /// <summary>
        /// 使用密码登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public JsonResult LoginByPassword(string userName, string password, bool rememberMe)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return JsonParamsErrorResult(nameof(userName));
            if (string.IsNullOrEmpty(password))
                return JsonParamsErrorResult(nameof(password));

            var user = DbContext.Users.Single(x => x.UserName == userName);
            if (user == null)
                return JsonBusinessErrorResult("用户名不存在");
            if (user.PasswordHash != Hash.GetMd5(password))
                return JsonBusinessErrorResult("密码错误");
            if (user.IsDisabled)
                return JsonBusinessErrorResult("用户已被禁用");

            if (user.GooleAuthEnabled)
            {
                HttpContext.Session.Set("userName", Encoding.UTF8.GetBytes(user.UserName));
                HttpContext.Session.Set("rememberMe", new byte[] { Convert.ToByte(rememberMe) });
            }
            else
                SignIn(user);

            return JsonSuccessResult(new { twoFactorEnabled = user.GooleAuthEnabled });
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.ManageSignOut();
            return RedirectToAction(nameof(Login));
        }

        /// <summary>
        /// 启用令牌验证器页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult EnableAuthenticator()
        {
            return View();
        }

        /// <summary>
        /// 获取令牌验证器二维码地址
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> GetAuthenticatorUri()
        {
            if (!HttpContext.Session.TryGetValue("userName", out var userNameBytes))
                return JsonBusinessErrorResult("会话已超时，请重新登录");
            var user = DbContext.Users.SingleOrDefault(x => x.UserName == Encoding.UTF8.GetString(userNameBytes));

            if (string.IsNullOrWhiteSpace(user.GoogleAuthSecretKey))
            {
                user.GoogleAuthSecretKey = GoogleAuthenticatorHelper.GenerateNewGoogleAuthenticatorSecretKey();
                DbContext.Update(user);
                await DbContext.SaveChangesAsync();
            }

            var qrcodeUrl = GoogleAuthenticatorHelper.GetAuthenticatorUrl(user.GoogleAuthSecretKey, user.UserName, _configuration["SiteConfig:AppName"]);

            return JsonSuccessResult(qrcodeUrl);
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        private void SignIn(AspNetUser user)
        {
            HttpContext.ManageSignIn(user.Id, user.UserName);
            Task.WaitAll(DbContext.ExecuteNonQueryAsync($"UPDATE AspNetUser SET LastLoginTime='{DateTime.UtcNow}' WHERE Id={user.Id}"));
        }

        /// <summary>
        /// 验证登陆token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public JsonResult VerifyToken(string token)
        {
            if (!HttpContext.Session.TryGetValue("userName", out var userNameBytes) || !HttpContext.Session.TryGetValue("rememberMe", out var rememberMeBytes))
                return JsonBusinessErrorResult("会话已超时，请重新登录");
            var user = DbContext.Users.SingleOrDefault(x => x.UserName == Encoding.UTF8.GetString(userNameBytes));
            var rememberMe = Convert.ToBoolean(rememberMeBytes[0]);
            if (user == null)
                return JsonBusinessErrorResult("用户名不存在");
            if (user.IsDisabled)
                return JsonBusinessErrorResult("用户已被禁用");

            var b = GoogleAuthenticatorHelper.ValidateGoogleAuthenticatorToken(user.GoogleAuthSecretKey, token);
            if (!b)
                return JsonBusinessErrorResult("验证失败");

            SignIn(user);
            return JsonSuccessResult(b);
        }
    }
}
