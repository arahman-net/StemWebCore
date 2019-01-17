using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("AddressType", Schema = "Core")]
    public class AddressType : CompanyEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
