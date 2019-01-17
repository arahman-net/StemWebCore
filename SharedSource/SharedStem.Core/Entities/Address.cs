using Stem.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("Address", Schema = "Core")]
    public class Address : BaseEntity
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public int? CityId { get; set; }
        public virtual City City { get; set; }

        public string ZipCode { get; set; }

        public int? StateProvinceId { get; set; }
        public virtual StateProvince StateProvince { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        public bool IsPrimary { get; set; }

        //public int BusinessEntityId { get; set; }
        //public virtual BusinessEntity BusinessEntity { get; set; }

        public int? AddressTypeId { get; set; }
        public virtual AddressType AddressType { get; set; }

        
        
        

    }
}
