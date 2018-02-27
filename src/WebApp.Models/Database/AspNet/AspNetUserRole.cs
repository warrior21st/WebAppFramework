using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApp.Models.Database.AspNet
{
    /// <summary>
    /// 用户角色表
    /// </summary>
    public class AspNetUserRole: IdentityUserRole<string>
    {
        public AspNetUserRole() : base()
        {
            
        }
    }
}
