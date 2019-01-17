using SharedStem.Core.Entities.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedStem.Core.Entities.Security
{
    [Table("SecurityGroupFeature", Schema = "Core")]
    public class SecurityGroupFeature : CompanyEntity
    {
        public int AppFeatureId { get; set; }
        public virtual AppFeature AppFeature { get; set; }
        public int SecurityGroupId { get; set; }
        public virtual SecurityGroup SecurityGroup { get; set; }
        
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Admin { get; set; }

    }
}
