using SharedStem.Core.Entities.Security;
using Stem.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedStem.Core.Entities.App
{
    [Flags]
    public enum PermissionTypes
    {
        CreateCase = (1 << 0),
        ViewCase = (1 << 1),
        DeleteCase = (1 << 2),
        Admin = (CreateCase | ViewCase | DeleteCase)
    }

    [Table("AppFeature", Schema = "App")]
    public class AppFeature : BaseEntity
    {   
        public AppFeature()
        {
            FeatureModules = new HashSet<AppModuleFeature>();
            FeatureGroups = new HashSet<SecurityGroupFeature>();
        }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsInternal { get; set; }
        public ICollection<AppModuleFeature> FeatureModules { get; set; }
        public virtual ICollection<SecurityGroupFeature> FeatureGroups { get; set; }
    }
}
