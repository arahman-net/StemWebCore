using Stem.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("StateProvince", Schema = "Core")]
    public class StateProvince : BaseEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
