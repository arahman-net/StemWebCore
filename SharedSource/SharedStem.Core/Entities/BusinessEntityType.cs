using Stem.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("BusinessEntityType", Schema = "Core")]
    public class BusinessEntityType : BaseEntity
    {
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
