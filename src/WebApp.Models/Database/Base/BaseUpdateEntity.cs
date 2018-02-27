using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Models.Database.Base
{
    public abstract class BaseUpdateEntity : BaseEntity
    {
        /// <summary>
        /// 最后更新
        /// </summary>
        public DateTime LastUpdate { get; set; }
    }
}
