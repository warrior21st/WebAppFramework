using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

#pragma warning disable 1591
namespace DDomain
{
    public static  class IHtmlHelperExtensions
    {

        public static string IsSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return string.Compare(controller,currentController,true) == 0 &&
                string.Compare(action, currentAction, true) == 0 ?
                cssClass : String.Empty;
        }

        public static string PageClass(this IHtmlHelper htmlHelper)
        {
            string currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        /// <summary>
        /// 返回已读类
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="unReadcount"></param>
        /// <returns></returns>
        public static string UnreadClass(this IHtmlHelper htmlHelper, int? unReadcount)
        {
            if(unReadcount  == null || unReadcount <= 0)
            {
                return "read";
            }
            else
            {
                return "unread";
            }

        }

    }
}
