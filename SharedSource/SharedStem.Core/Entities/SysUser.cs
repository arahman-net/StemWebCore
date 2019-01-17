using SharedStem.Core.Entities.Security;
using Stem.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("SysUser", Schema = "Core")]
    public class SysUser : BaseEntity
    {
        public SysUser()
        {
            AuthProviders = new HashSet<UserAuthProvider>();
            Settings = new HashSet<UserSetting>();
            SecurityGroups = new List<UserSecurityGroup>();
            UserCompanies = new HashSet<CompanyUser>();
            SecurityGroupIds = new List<int>();
        }
        public string AuthId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public string Password { get; set; }
        public virtual ICollection<UserAuthProvider> AuthProviders { get; set; }
        public virtual ICollection<UserSetting> Settings { get; set; }
        public virtual IList<UserSecurityGroup> SecurityGroups { get; set; }
        public virtual ICollection<CompanyUser> UserCompanies { get; set; }

        [NotMapped]
        public ICollection<int> SecurityGroupIds { get; set; }

    }
}
