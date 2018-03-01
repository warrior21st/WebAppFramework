using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Models.Database.Base;

namespace WebApp.Models.Database
{
    /// <summary>
    /// 接口操作表
    /// </summary>
    [Table("InterfaceOperation")]
    public class InterfaceOperation : BaseEntity
    {
        /// <summary>
        /// 所属组
        /// </summary>
        [MaxLength(255)]
        public string GroupName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// 父级名称
        /// </summary>
        [MaxLength(255)]
        public string ParentName { get; set; }        
    }
}
