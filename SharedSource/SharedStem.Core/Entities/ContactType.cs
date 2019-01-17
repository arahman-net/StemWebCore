using Stem.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("ContactType", Schema = "Core")]
    public class ContactType : CompanyEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
