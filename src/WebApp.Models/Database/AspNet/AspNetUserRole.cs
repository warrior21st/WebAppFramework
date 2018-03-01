using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Models.Database.Base;

namespace WebApp.Models.Database.AspNet
{
    /// <summary>
    /// 用户角色表
    /// </summary>
    public class AspNetUserRole : BaseEntity
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
