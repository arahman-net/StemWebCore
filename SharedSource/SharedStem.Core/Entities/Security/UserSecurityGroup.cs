
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities.Security
{
    [Table("UserSecurityGroup", Schema = "Core")]
    public class UserSecurityGroup : CompanyEntity
    {
        public int SysUserId { get; set; }
        public SysUser SysUser { get; set; }

        public int SecurityGroupId { get; set; }
        public SecurityGroup SecurityGroup { get; set; }
       
    }
}
