using WebApp.Models.Database.AspNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Models.ManageUser
{
    public class UserViewModel
    {
        public AspNetUser User { get; set; }

        public AspNetRole Role { get; set; }
    }
}
