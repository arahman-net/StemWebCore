using Stem.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedStem.Core.Entities
{
    [Table("CompanyUser", Schema = "Core")]
    public class CompanyUser: CompanyEntity
    {
        public int SysUserId { get; set; }
        public virtual SysUser SysUser { get; set; }
    }
}
