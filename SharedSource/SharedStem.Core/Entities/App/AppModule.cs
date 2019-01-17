using Stem.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedStem.Core.Entities.App
{
    [Table("AppModule", Schema = "App")]
    public class AppModule : BaseEntity
    {
        public AppModule()
        {
            ModuleCompanies = new HashSet<CompanyModule>();
            ModuleFeatures = new HashSet<AppModuleFeature>();
        }

        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsInternal { get; set; }

        public ICollection<CompanyModule> ModuleCompanies { get; set; }
        public ICollection<AppModuleFeature> ModuleFeatures { get; set; }

        [NotMapped]
        public ICollection<int> ModuleFeatureIds { get; set; }
    }
}
