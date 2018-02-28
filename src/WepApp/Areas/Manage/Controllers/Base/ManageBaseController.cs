using WebApp.Controllers;
using WebApp.Models.Database.AspNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Authorization;

namespace WebApp.Areas.Manage.Controllers
{
    public abstract class ManageBaseController : BaseController
    {
        public ManageBaseController(ILogger<ManageBaseController> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {

        }

        protected override string ActionDefaultUri
        {
            get
            {
                return $"/Manage{base.ActionDefaultUri}";
            }
        }

        private string _currentInterfaceName;

        /// <summary>
        /// 授权接口名
        /// </summary>
        protected string CurrentInterfaceName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_currentInterfaceName))
                    _currentInterfaceName = HttpContext.GetRequestInterfaceName();
                return _currentInterfaceName;
            }
        }

        private List<InterfaceOperationModel> _userAllInterfaceAuthorities;

        /// <summary>
        /// 用户所有权限
        /// </summary>
        protected List<InterfaceOperationModel> UserAllInterfaceAuthorities
        {
            get
            {
                if (_userAllInterfaceAuthorities == null)
                    _userAllInterfaceAuthorities = _serviceProvider.GetService<AuthorityManager>().GetUserAuthoritiesAsync(CurrentLoginUser).GetAwaiter().GetResult();

                return _userAllInterfaceAuthorities;
            }
        }

        private string[] _userCurrentInterfaceAuthorities;

        /// <summary>
        /// 当前用户拥有的当前接口操作权限
        /// </summary>
        protected string[] UserCurrentInterfaceAuthorities
        {
            get
            {
                if (_userCurrentInterfaceAuthorities == null)
                {
                    if (!IsLogined)
                        _userCurrentInterfaceAuthorities = new string[] { };
                    else
                        _userCurrentInterfaceAuthorities = UserAllInterfaceAuthorities.Where(x => x.InterfaceName == CurrentInterfaceName).Select(x => x.OperationName).ToArray();
                }

                return _userCurrentInterfaceAuthorities;
            }
        }

        private AspNetUser _currentLoginUser;

        /// <summary>
        /// 当前登录的用户
        /// </summary>
        protected AspNetUser CurrentLoginUser
        {
            get
            {
                if (IsLogined && _currentLoginUser == null)
                    _currentLoginUser = DbContext.Users.Single(x => x.UserName == User.Identity.Name);

                return _currentLoginUser;
            }
        }

        private string _currentLoginUserRoleName;

        protected string CurrentLoginUserRoleName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_currentLoginUserRoleName))
                {
                    var ur = DbContext.UserRoles.First(x => x.UserId == CurrentLoginUser.Id);
                    var role = DbContext.Roles.Single(x => x.Id == ur.RoleId);
                    _currentLoginUserRoleName = role.Name;
                }

                return _currentLoginUserRoleName;
            }
        }

        /// <summary>
        /// 是否已登录
        /// </summary>
        protected bool IsLogined
        {
            get
            {
                return User != null && User.Identity != null && !string.IsNullOrWhiteSpace(User.Identity.Name);
            }
        }

        #region view override

        private void AddViewData()
        {
            if (!string.IsNullOrWhiteSpace(CurrentInterfaceName))
            {
                ViewData["UserInterfaceAuthorities"] = UserCurrentInterfaceAuthorities ?? new string[] { };
                //ViewData["isAdmin"] = _currentLoginUserRoleName == RoleTypes.Admin.ToString();
            }
        }

        public override ViewResult View()
        {
            AddViewData();
            return base.View();
        }

        public override ViewResult View(object model)
        {
            AddViewData();
            return base.View(model);
        }

        public override ViewResult View(string viewName)
        {
            AddViewData();
            return base.View(viewName);
        }

        public override ViewResult View(string viewName, object model)
        {
            AddViewData();
            return base.View(viewName, model);
        }

        #endregion

        ///// <summary>
        ///// 添加日志
        ///// </summary>
        //private void AddManagerLog()
        //{
        //    if (IsLogined && !string.IsNullOrWhiteSpace(HttpContext.GetLogOperationName()))
        //    {
        //        var opName = HttpContext.GetLogOperationName();
        //        var userName = User.Identity.Name;
        //        var paras = new Dictionary<string, object>();
        //        if (HttpContext.Request.Method.CompareIgnoreCase("get"))
        //        {
        //            foreach (var key in HttpContext.Request.Query.Keys)
        //            {
        //                paras.Add(key, HttpContext.Request.Query[key]);
        //            }
        //        }
        //        else if (HttpContext.Request.Method.CompareIgnoreCase("post"))
        //        {
        //            foreach (var key in HttpContext.Request.Form.Keys)
        //            {
        //                paras.Add(key, HttpContext.Request.Form[key]);
        //            }
        //        }
        //        _serviceProvider.GetService<OperationLogService>().AddManagerLog(userName, opName, paras);
        //    }
        //}

        /// <summary>
        /// 生成json返回结果
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected override JsonResult GetJsonResult(string msg, object data, int statusCode)
        {
            //if (statusCode == StatusCodes.Status200OK)
            //    AddManagerLog();

            return base.GetJsonResult(msg, data, statusCode);
        }
    }
}
