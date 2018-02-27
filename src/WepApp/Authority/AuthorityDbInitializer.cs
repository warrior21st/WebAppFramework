using WebApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Models.Database;
using System.ComponentModel;
using WebApp.Models.Authority;

namespace WebApp.Authority
{
    /// <summary>
    /// 权限数据初始化类
    /// </summary>
    public static class AuthorityDbInitializer
    {
        /// <summary>
        /// 初始化接口集合
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task InitInterfaceOperations(IServiceProvider serviceProvider)
        {
            var baseType = typeof(IManageInterface);
            Assembly assembly = baseType.GetTypeInfo().Assembly;
            var types = assembly.GetTypes()
                        .Where(t => t.GetInterfaces().Contains(baseType))
                        .ToList();

            if (types != null && types.Count > 0)
            {
                var context = serviceProvider.GetService<AppDbContext>();
                var existInterfaceNames = context.InterfaceOperations.Where(x => x.ParentName == null).Select(x => x.Name).ToList();
                var existOpers = await context.QueryListBySqlAsync<InterfaceOperationModel>(AuthorityManager.GET_ALL_OPERATION_SQL);
                foreach (var t in types)
                {
                    var groupDesc = t.GetCustomAttribute<InterfaceGroupAttribute>();
                    if (!existInterfaceNames.Contains(t.Name))
                    {
                        if (groupDesc != null)
                        {
                            var model = new InterfaceOperation()
                            {
                                CreateTime = DateTime.UtcNow,
                                LastUpdate = DateTime.UtcNow,
                                Name = t.Name,
                                GroupName = groupDesc.GroupName,
                                Description = groupDesc.Description,
                                ParentName = null
                            };

                            context.InterfaceOperations.Add(model);
                        }
                    }

                    var fieldInfos = t.GetFields();
                    var list = new List<InterfaceOperation>();
                    foreach (var f in fieldInfos)
                    {
                        var operName = f.GetRawConstantValue().ToString();
                        if (!string.IsNullOrWhiteSpace(operName) && existOpers.Count(x => x.InterfaceName == t.Name && x.OperationName == operName) == 0)
                        {
                            var desc = f.GetCustomAttribute<DescriptionAttribute>();
                            if (desc != null)
                            {
                                var oper = new InterfaceOperation()
                                {
                                    CreateTime = DateTime.UtcNow,
                                    LastUpdate = DateTime.UtcNow,
                                    ParentName = t.Name,
                                    GroupName = groupDesc.GroupName,
                                    Description = desc.Description,
                                    Name = f.GetRawConstantValue().ToString()
                                };
                                if (oper.Name.CompareIgnoreCase("read") && list.Count > 0)
                                    list.Insert(0, oper);
                                else if (oper.Name.CompareIgnoreCase("write") && list.Count > 1)
                                    list.Insert(1, oper);
                                else if (oper.Name.CompareIgnoreCase("delete") && list.Count > 2)
                                    list.Insert(2, oper);
                                else
                                    list.Add(oper);
                            }                            
                        }
                    }
                    if (list.Count > 0)
                        context.InterfaceOperations.AddRange(list);

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
