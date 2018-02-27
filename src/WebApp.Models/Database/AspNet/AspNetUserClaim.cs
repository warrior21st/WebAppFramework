﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Database.AspNet
{
    public class AspNetUserClaim : IdentityUserClaim<string>
    {
        public AspNetUserClaim() : base()
        {

        }
    }
}
