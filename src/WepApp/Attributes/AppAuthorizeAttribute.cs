using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Database;

namespace WebApp.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AppAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly ActionResultTypes _resultType;

        public AppAuthorizeAttribute()
        {
            _resultType = ActionResultTypes.ViewResult;
        }

        public AppAuthorizeAttribute(ActionResultTypes resultType)
        {
            _resultType = resultType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            #if DEBUG
                if (!context.HttpContext.AppIsLogin())
                    context.HttpContext.AppSignIn(new AppUser() { Id = 2 });
            #endif

            if (!context.HttpContext.AppIsLogin())
            {
                if (_resultType == ActionResultTypes.ViewResult)
                    context.Result = new RedirectResult($"/login?returnUrl={context.HttpContext.Request.Path.Value}");
                else if (_resultType == ActionResultTypes.JsonResult)
                    context.Result = new JsonResult(null) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
