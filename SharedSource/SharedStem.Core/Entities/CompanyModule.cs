using SharedStem.Core.Entities.App;
using Stem.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedStem.Core.Entities
{
    [Table("CompanyModule", Schema = "Core")]
    public class CompanyModule : CompanyEntity
    {   
        public int AppModuleId { get; set; }
        public virtual AppModule AppModule { get; set; }
    }
}
