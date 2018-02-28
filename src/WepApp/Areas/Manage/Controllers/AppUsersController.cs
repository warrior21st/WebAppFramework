using DDomain.DAL.Helpers;
using DDomain.Models;
using DDomain.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Authority;

namespace WebApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class AppUsersController : ManageBaseController
    {
        public AppUsersController(ILogger<AppUsersController> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {

        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorityVerify(nameof(ManageConsumer), ManageConsumer.READ)]
        public async Task<IActionResult> List(string search, string sort = "CreateTime", string order = QueryOptions.ORDER_DESC, int pageIndex = 1, int pageSize = PagingModel.MIN_PAGE_SIZE)
        {
            var list = new List<AppUser>();
            var walletCounts = new List<dynamic>();
            var paging = GetCommonPagingModel(search, sort, order, pageIndex, pageSize);
            var query = DbContext.AppUsers.Where(x => 1 == 1);
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.AliasName.Contains(search));
            paging.Total = query.Count();
            if (paging.Total > 0)
            {
                var options = new QueryOptions(sort, order, paging.Start, paging.Limit);
                if (EntityHelper.HasColumn<DDomainGoods>(options.SortColumn))
                    query = query.AddOrderBy(options.SortColumn, options.Order);

                list = query.Skip(options.Start).Take(options.Limit).ToList();

                walletCounts = await DbContext.QueryDynamicListBySqlAsync($"SELECT Uid AS Id,COUNT(1) AS Count FROM DDomainEthAccount WHERE Uid IN ({list.Select(x => x.Id).ToList().GetSqlConditionString()}) GROUP BY Uid");
            }

            ViewData["paging"] = paging;
            ViewData["tokens"] = DbContext.PayTokens.ToList();
            ViewData["walletCounts"] = walletCounts;
            ViewData["languages"] = DbContext.Languages.ToList();

            return View(list);
        }

        /// <summary>
        /// 用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorityVerify(nameof(ManageConsumer), ManageConsumer.READ)]
        public IActionResult Detail(int id)
        {
            if (id <= 0)
                return JsonParamsErrorResult(nameof(id));
            var user = DbContext.AppUsers.SingleOrDefault(x => x.Id == id);
            if (user == null)
                return JsonBusinessErrorResult("id不存在或已被删除");
            var wallets = DbContext.EthAccounts.Where(x => x.Uid == id).ToList();
            ViewData["wallets"] = wallets;
            return View(user);
        }

        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorityVerify(nameof(ManageAppLoginLog), ManageAppLoginLog.READ)]
        public IActionResult LoginLogs(int id = 0, string sort = "CreateTime", string order = QueryOptions.ORDER_DESC, int pageIndex = 1, int pageSize = PagingModel.MIN_PAGE_SIZE)
        {
            var list = new List<DDomainAppLoginLog>();
            var appUsers = new List<DDomainAppUser>();
            var paging = GetCommonPagingModel(null, sort, order, pageIndex, pageSize);
            var query = DbContext.AppLoginLogs.Where(x => 1 == 1);
            if (id > 0)
                query = query.Where(x => x.Uid == id);
            paging.Total = query.Count();
            if (paging.Total > 0)
            {
                var options = new QueryOptions(sort, order, paging.Start, paging.Limit);
                if (EntityHelper.HasColumn<DDomainAppLoginLog>(options.SortColumn))
                    query = query.AddOrderBy(options.SortColumn, options.Order);

                list = query.Skip(options.Start).Take(options.Limit).ToList();
                var uids = list.Select(x => x.Uid).Distinct().ToList();
                appUsers = DbContext.AppUsers.Where(x => uids.Contains(x.Id)).ToList();
            }

            ViewData["paging"] = paging;
            ViewData["appUsers"] = appUsers;

            return View(list);
        }
    }
}
