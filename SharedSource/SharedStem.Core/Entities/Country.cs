using Stem.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("Country", Schema = "Core")]
    public class Country : BaseEntity
    {
        public Country() { }

        public string Name { get; set; }
        public string ShortName { get; set; }
        public string CountryCode { get; set; }
    }
}
