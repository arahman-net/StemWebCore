using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("BusinessEntity", Schema = "Core")]
    public class BusinessEntity : CompanyEntity
    {
        public BusinessEntity()
        {
            Contacts = new HashSet<Contact>();
            Addresses = new HashSet<Address>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string WebSite { get; set; }

        public int? BusinessEntityTypeId { get; set; }
        public BusinessEntityType BusinessEntityType { get; set; }
        
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        
    }
}
