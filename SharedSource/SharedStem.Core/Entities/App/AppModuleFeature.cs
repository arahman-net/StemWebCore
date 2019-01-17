using Stem.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedStem.Core.Entities.App
{
    [Table("AppModuleFeature", Schema = "App")]
    public class AppModuleFeature : BaseEntity
    {
        public int AppModuleId { get; set; }
        public virtual AppModule AppModule { get; set; }
        public int AppFeatureId { get; set; }
        public virtual AppFeature AppFeature { get; set; }
        public string GroupName { get; set; }
        public string SubGroupName { get; set; }

    }
}
