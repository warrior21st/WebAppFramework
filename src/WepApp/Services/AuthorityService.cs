using WebApp.DAL;
using WebApp.Models.Authorization;
using WebApp.Models.Database;
using WebApp.Models.Database.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public class AuthorityService
    {
        private readonly AppDbContext _context;

        public AuthorityService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<AspNetRole, List<InterfaceOperation>> GetRoleAuthorities()
        {
            var dic = new Dictionary<AspNetRole, List<InterfaceOperation>>();
            var operations = _context.InterfaceOperations.ToList();
            var authorities = _context.Authorities.ToList();
            var roles = _context.Roles.ToList();
            foreach (var r in roles)
            {
                var roleOperaIds = authorities.Where(x => x.AuthorityId == r.AuthorityId).Select(x => x.OperationId).ToList();
                var roleOperations = operations.Where(x => roleOperaIds.Contains(x.Id)).ToList();

                dic.Add(r, roleOperations);
            }

            return dic;
        }

        public List<InterfaceOperation> GetAuthoritiesByAuthorityId(string authorityId)
        {
            var auths = _context.Authorities.Where(x => x.AuthorityId == authorityId);
            var operationIds = auths.Select(x => x.OperationId).ToList();
            var operations = _context.InterfaceOperations.Where(x => operationIds.Contains(x.Id)).ToList();

            return operations;
        }

        /// <summary>
        /// 获取所有权限模型
        /// </summary>
        /// <returns></returns>
        public List<AuthorityModel> GetAllAuthorityModels()
        {
            var operations = _context.InterfaceOperations.ToList();

            return GetAuthorityModels(operations);
        }

        /// <summary>
        /// 生成权限模型列表
        /// </summary>
        /// <param name="operations"></param>
        /// <returns></returns>
        private List<AuthorityModel> GetAuthorityModels(List<InterfaceOperation> operations)
        {
            var list = new List<AuthorityModel>();
            var interfaceNames = operations.Select(x => x.ParentName).ToList();
            var interfaces = _context.InterfaceOperations.Where(x => interfaceNames.Contains(x.Name)).ToList();
            var groups = interfaces.Select(x => x.GroupName).Distinct().ToList();
            foreach (var g in groups)
            {
                var model = new AuthorityModel();
                model.GroupName = g;
                var groupInterfaces = interfaces.Where(x => x.GroupName == g).ToList();
                foreach (var dexinterface in groupInterfaces)
                {
                    var interfaceOperations = operations.Where(x => x.ParentName == dexinterface.Name).ToList();
                    var modelInterface = new InterfaceModel();
                    modelInterface.InterfaceName = dexinterface.Name;
                    modelInterface.InterfaceDescription = dexinterface.Description;
                    foreach (var op in interfaceOperations)
                    {
                        modelInterface.Operations.Add(new OperationModel()
                        {
                            Id = op.Id,
                            Name = op.Name,
                            Description = op.Description
                        });
                    }

                    model.Interfaces.Add(modelInterface);
                }

                list.Add(model);
            }

            return list;
        }
    }
}
