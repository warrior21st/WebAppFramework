using WebApp.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger<BaseController> _logger;
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IConfiguration _configuration;

        public BaseController(ILogger<BaseController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = _serviceProvider.GetService<IConfiguration>();
        }

        private AppDbContext _dbContext;

        /// <summary>
        /// 数据库连接
        /// </summary>
        protected AppDbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                    _dbContext = _serviceProvider.GetService<AppDbContext>();

                return _dbContext;
            }
        }

        /// <summary>
        /// 获取普通分页模型
        /// </summary>
        /// <param name="search"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        protected PagingModel GetCommonPagingModel(string search, string sort, string order, int pageIndex, int pageSize)
        {
            return GetPagingModel(ActionDefaultUri, search, sort, order, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取分页模型
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="search"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        protected virtual PagingModel GetPagingModel(string requestUrl, string search, string sort, string order, int pageIndex, int pageSize)
        {
            var paging = new PagingModel();
            paging.PageIndex = pageIndex;
            paging.PageSize = pageSize;
            paging.RequestUrl = requestUrl;
            if (!string.IsNullOrWhiteSpace(search))
                paging.QueryParams.Add(nameof(search), search);
            if (!string.IsNullOrWhiteSpace(sort))
                paging.QueryParams.Add(nameof(sort), sort);
            if (!string.IsNullOrWhiteSpace(order))
                paging.QueryParams.Add(nameof(order), order);

            return paging;
        }

        /// <summary>
        /// 当前接口默认uri
        /// </summary>
        protected virtual string ActionDefaultUri
        {
            get
            {
                var controller = ControllerContext.RouteData.Values["controller"].ToString();
                var action = ControllerContext.RouteData.Values["action"].ToString();
                return RouteData.Values.ContainsKey("id") ? $"/{controller}/{action}/{RouteData.Values["id"].ToString()}" : $"/{controller}/{action}";
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var restrictControllers = _configuration.GetSection("SiteConfig").GetSection("RestrictRequestControllers").GetChildren().Select(x => x.Value).ToArray();
            var controller = (string)context.RouteData.Values["controller"];
            if (restrictControllers.Length > 0 && (restrictControllers.Count(x => x.ContainsIgnoreCase(controller)) == 0))
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                _logger.LogError(context.Exception);

                context.ExceptionHandled = true;
                context.Result = JsonFailureResult(context.Exception.Message);
                context.Exception = null;
            }

            base.OnActionExecuted(context);
        }

        /// <summary>
        /// 生成成功json结果
        /// </summary>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected JsonResult JsonSuccessResult(object data = null, int statusCode = StatusCodes.Status200OK)
        {
            return GetJsonResult("ok", data, statusCode);
        }

        /// <summary>
        /// 生存错误请求json结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected virtual JsonResult JsonBusinessErrorResult(string msg)
        {
            return JsonFailureResult(msg, StatusCodes.Status500InternalServerError);
        }

        protected virtual JsonResult JsonParamsErrorResult(string paramName)
        {
            //var obj = new
            //{
            //    text = "paramsErrorResult",
            //    parameters = new List<string>() { paramName }
            //};
            return JsonBadRequest($"参数{paramName}不正确");
        }

        protected JsonResult JsonBadRequest(string msg, int statusCode = StatusCodes.Status400BadRequest)
        {
            return JsonFailureResult(msg, statusCode);
        }

        protected JsonResult JsonAssessDeniedResult(string msg = "权限不足")
        {
            return JsonFailureResult(msg, StatusCodes.Status403Forbidden);
        }

        /// <summary>
        /// 生成失败json结果
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected JsonResult JsonFailureResult(string msg, int statusCode = StatusCodes.Status500InternalServerError)
        {
            return GetJsonResult(msg, null, statusCode);
        }

        /// <summary>
        /// 生成json返回结果
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected virtual JsonResult GetJsonResult(string msg, object data, int statusCode)
        {
            return new JsonResult(new { msg = msg, data = data }) { StatusCode = statusCode };
        }
    }
}
