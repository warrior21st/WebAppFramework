using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using WebApp.Models.Database;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.Models;
using WebApp.Models.View;
using CommonHelpers.Helpers;

namespace WebApp.Controllers
{
    public abstract class SiteBaseController : BaseController
    {
        public SiteBaseController(ILogger<SiteBaseController> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {

        }

        private int _loginUserId;

        /// <summary>
        /// 当前登录的用户id
        /// </summary>
        protected virtual int LoginUserId
        {
            get
            {
                if (_loginUserId <= 0)
                {
                    var user = HttpContext.GetAppLoginUser();
                    _loginUserId = user != null ? user.Id : 0;
                }

                return _loginUserId;
            }
        }

        private AppUser _loginUser;

        /// <summary>
        /// 当前用户
        /// </summary>
        protected AppUser LoginUser
        {
            get
            {
                if (_loginUser == null)
                    _loginUser = DbContext.AppUsers.Single(x => x.Id == LoginUserId);

                return _loginUser;
            }
        }

        #region View override

        public override ViewResult View()
        {
            SetCommonViewData();
            return base.View();
        }

        public override ViewResult View(object model)
        {
            SetCommonViewData();
            return base.View(model);
        }

        public override ViewResult View(string viewName)
        {
            SetCommonViewData();
            return base.View(viewName);
        }

        public override ViewResult View(string viewName, object model)
        {
            SetCommonViewData();
            return base.View(viewName, model);
        }

        private void SetCommonViewData()
        {
            ViewData["Nickname"] = HttpContext.AppIsLogin() ? LoginUser.AliasName : null;
        }

        #endregion            
    }
}
