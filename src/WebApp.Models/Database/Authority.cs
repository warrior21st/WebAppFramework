using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Models.Database.Base;

namespace WebApp.Models.Database
{
    /// <summary>
    /// 权限表
    /// </summary>
    [Table("Authority")]
    public class Authority:BaseUpdateEntity
    {
        /// <summary>
        /// 权限标识
        /// </summary>
        [MaxLength(36)]
        public string AuthorityId { get; set; }

        /// <summary>
        /// 操作id
        /// </summary>
        public int OperationId { get; set; }
    }
}
