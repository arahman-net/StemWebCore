using Stem.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("City", Schema = "Core")]
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int? StateProvinceId { get; set; }
        public virtual StateProvince StateProvince { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }
    }
}
