using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Attributes;

namespace WebApp.Areas.Manage.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ManageAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly ActionResultTypes _resultType;

        public ManageAuthorizeAttribute()
        {
            _resultType = ActionResultTypes.ViewResult;
        }

        public ManageAuthorizeAttribute(ActionResultTypes resultType)
        {
            _resultType = resultType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.ManageIsLogin())
            {
                if (_resultType == ActionResultTypes.ViewResult)
                    context.Result = new RedirectResult($"/Manage/Account/Login?returnUrl={context.HttpContext.Request.Path.Value}");
                else if (_resultType == ActionResultTypes.JsonResult)
                    context.Result = new JsonResult(null) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
