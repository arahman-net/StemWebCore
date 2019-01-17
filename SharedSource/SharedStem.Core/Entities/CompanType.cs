
using Stem.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("CompanyType", Schema = "Core")]
    public class CompanyType : BaseEntity
    {
        public CompanyType()
        {
            CompanyModules = new HashSet<CompanyModule>();
        }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CompanyModule> CompanyModules { get; set; }
    }
}
