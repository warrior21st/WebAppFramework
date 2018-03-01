using WebApp.Authorization;
using WebApp.DAL;
using WebApp.Models.Database;
using WebApp.Models.Database.AspNet;
using WebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.ManageUser;
using WebApp.Areas.Manage.Attributes;

namespace WebApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [ManageAuthorize]
    public class AuthorityController : ManageBaseController
    {
        private readonly AuthorityService _authorityService;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly AuthorityManager _authenticator;

        public AuthorityController(ILogger<AuthorityController> logger, IServiceProvider serviceProvider,
            AuthorityService authorityService,
            UserManager<AspNetUser> userManager,
            AuthorityManager authenticator) : base(logger, serviceProvider)
        {
            _authorityService = authorityService;
            _userManager = userManager;
            _authenticator = authenticator;
        }

        /// <summary>
        /// 角色权限列表
        /// </summary>
        /// <returns></returns>
        [AuthorityVerify(nameof(ManageRoleAuthorities), ManageRoleAuthorities.READ)]
        public IActionResult RoleAuthorityList()
        {
            var dic = _authorityService.GetRoleAuthorities();
            ViewData["allAuthorities"] = _authorityService.GetAllAuthorityModels();

            return base.View(dic);
        }

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="operationIds"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorityVerify(nameof(ManageRoleAuthorities), ManageRoleAuthorities.WRITE)]
        public async Task<JsonResult> SaveRoleAuthority(string role, List<int> operationIds)
        {
            if (string.IsNullOrWhiteSpace(role))
                return JsonParamsErrorResult(nameof(role));
            if (operationIds == null)
                return JsonParamsErrorResult(nameof(operationIds));

            var dexrole = DbContext.Roles.Single(x => x.Name == role);
            await UpdateAuthorities(dexrole.AuthorityId, operationIds);

            _authenticator.RemoveRoleAuthorityCache(dexrole);
            return JsonSuccessResult();
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorityVerify(nameof(ManageUser), ManageUser.READ)]
        public async Task<IActionResult> Users()
        {
            var users = DbContext.Users.ToList();
            var list = new List<UserViewModel>();
            foreach (var user in users)
            {
                var roles = await DbContext.QueryListBySqlAsync<AspNetRole>($"SELECT a.* FROM `aspnetroles` a, `aspnetuserroles` b WHERE a.Id=b.RoleId AND b.UserId='{user.Id}'");
                list.Add(new UserViewModel()
                {
                    User = user,
                    Role = roles.First()
                });
            }

            ViewData["allRoles"] = DbContext.Roles.ToList();

            return View(list);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="role"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorityVerify(nameof(ManageUser), ManageUser.WRITE)]
        public async Task<JsonResult> AddUser(string role, AspNetUser user)
        {
            return await AddOrUpdateUser(role, user);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="role"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorityVerify(nameof(ManageUser), ManageUser.WRITE)]
        public async Task<JsonResult> UpdateUser(string role, AspNetUser user)
        {
            return await AddOrUpdateUser(role, user);
        }

        /// <summary>
        /// 添加/修改用户
        /// </summary>
        /// <param name="role"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<JsonResult> AddOrUpdateUser(string role, AspNetUser user)
        {
            var oldUser = new AspNetUser();
            if (user.Id <= 0)
                oldUser = DbContext.Users.Single(x => x.Id == user.Id);
            else
            {
                oldUser.CreateTime = DateTime.UtcNow;
                oldUser.AuthorityId = Guid.NewGuid().ToString();
                oldUser.UserName = user.UserName;
            }

            oldUser.IsDisabled = user.IsDisabled;

            if (DbContext.Users.Count(x => x.UserName == oldUser.UserName && x.Id != oldUser.Id) > 0)
                return JsonBusinessErrorResult("用户名已被使用");

            if (user.Id <= 0)
            {
                var res = await _userManager.CreateAsync(oldUser, user.PasswordHash);
                if (!res.Succeeded)
                    return JsonFailureResult($"保存失败:{res.Errors.FirstOrDefault()?.Description}");
            }
            else
            {
                DbContext.Update(oldUser);
                await DbContext.SaveChangesAsync();
            }
            var roles = await _userManager.GetRolesAsync(oldUser);
            if (!roles.Contains(role))
            {
                var res1 = await _userManager.RemoveFromRolesAsync(oldUser, roles);
                if (!res1.Succeeded)
                    return JsonFailureResult($"保存失败:{res1.Errors.FirstOrDefault()?.Description}");
                var res2 = await _userManager.AddToRoleAsync(oldUser, role);
                if (!res2.Succeeded)
                    return JsonFailureResult($"保存失败:{res1.Errors.FirstOrDefault()?.Description}");
            }

            return JsonSuccessResult();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorityVerify(nameof(ManageUser), ManageUser.DELETE)]
        public async Task<JsonResult> DeleteUsers(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return JsonParamsErrorResult(nameof(ids));

            var count = DbContext.Users.Count();
            if (ids.Count >= count)
                return JsonBusinessErrorResult("系统需要保留至少一位用户");

            var users = DbContext.Users.Where(x => ids.Contains(x.Id)).ToList();
            foreach (var u in users)
            {
                var res = await _userManager.DeleteAsync(u);
            }

            return JsonSuccessResult();
        }

        /// <summary>
        /// 用户权限管理
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorityVerify(nameof(ManageUser), ManageUser.SET_USER_AUTHORITY)]
        public async Task<IActionResult> UserAuthorityManage(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return JsonParamsErrorResult(nameof(userName));

            var user = DbContext.Users.SingleOrDefault(x => x.UserName == userName);
            if (user == null)
                return JsonBusinessErrorResult("用户不存在或已被删除");

            var role = (await _userManager.GetRolesAsync(user)).First();
            var roleAuthId = DbContext.Roles.Single(x => x.Name == role).AuthorityId;

            var operations = _authorityService.GetAuthoritiesByAuthorityId(user.AuthorityId);
            ViewData["allAuthorities"] = _authorityService.GetAllAuthorityModels();
            ViewData["userRoleOperations"] = _authorityService.GetAuthoritiesByAuthorityId(roleAuthId);

            return View(new KeyValuePair<AspNetUser, List<InterfaceOperation>>(user, operations));
        }

        /// <summary>
        /// 修改用户权限
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="operationIds"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorityVerify(nameof(ManageUser), ManageUser.SET_USER_AUTHORITY)]
        public async Task<JsonResult> UpdateUserAuthorities(string userName, List<int> operationIds)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return JsonParamsErrorResult(nameof(userName));
            if (operationIds == null)
                return JsonParamsErrorResult(nameof(operationIds));

            var user = DbContext.Users.SingleOrDefault(x => x.UserName == userName);
            if (user == null)
                return JsonBusinessErrorResult("用户不存在或已被删除");

            await UpdateAuthorities(user.AuthorityId, operationIds);

            _authenticator.RemoveUserAuthorityCache(user);

            return JsonSuccessResult();
        }

        /// <summary>
        /// 修改操作权限
        /// </summary>
        /// <param name="authorityId"></param>
        /// <param name="operationIds"></param>
        /// <returns></returns>
        private async Task UpdateAuthorities(string authorityId, List<int> operationIds)
        {
            var list = DbContext.Authorities.Where(x => x.AuthorityId == authorityId).ToList();
            var existedIds = list.Where(x => operationIds.Contains(x.OperationId)).Select(x => x.OperationId).ToList();
            var shouldRemoveList = list.Where(x => !existedIds.Contains(x.OperationId)).ToList();
            var shouldAddIds = operationIds.Where(x => !existedIds.Contains(x)).ToList();
            foreach (var id in shouldAddIds)
            {
                var model = new Authority();
                model.AuthorityId = authorityId;
                model.CreateTime = DateTime.UtcNow;
                model.LastUpdate = DateTime.UtcNow;
                model.OperationId = id;

                DbContext.Authorities.Add(model);
            }

            if (shouldRemoveList.Count > 0)
                DbContext.Authorities.RemoveRange(shouldRemoveList);

            await DbContext.SaveChangesAsync();
        }
    }
}
