using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities.Security
{
    [Table("SecurityGroup", Schema = "Core")]
    public class SecurityGroup : CompanyEntity
    {
        public SecurityGroup()
        {
            GroupUsers = new HashSet<UserSecurityGroup>();
            GroupFeatures = new HashSet<SecurityGroupFeature>();
            FeaturePermissions = new List<FeaturePermission>();
        }
        public string GroupName { get; set; }
        public bool IsOwner { get; set; }
        public virtual ICollection<UserSecurityGroup> GroupUsers { get; set; }
        public virtual ICollection<SecurityGroupFeature> GroupFeatures { get; set; }

        [NotMapped]
        public ICollection<int> GroupFeatureIds { get; set; }

        [NotMapped]
        public List<FeaturePermission> FeaturePermissions { get; set; }
       
    }

    public class FeaturePermission
    {
        public int FeatureId { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Admin { get; set; }


    }
}
