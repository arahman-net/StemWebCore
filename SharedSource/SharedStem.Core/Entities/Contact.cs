using Stem.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("Contact", Schema = "Core")]
    public partial class Contact : CompanyEntity
    {
        public int? ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }
        public string NumberOrId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneHome { get; set; }
        public string PhoneWork { get; set; }
        public string Cell { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        
        public bool IsPrimary { get; set; }
    }
}
