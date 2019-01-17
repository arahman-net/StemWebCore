using Stem.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("Company", Schema = "Core")]
    public class Company : BaseEntity
    {
        public Company()
        {
            CompanyUsers = new HashSet<CompanyUser>();
        }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string CompanyCode { get; set; }
        public int? CompanyTypeId { get; set; }
        public CompanyType CompanyType { get; set; }
        public virtual ICollection<CompanyUser> CompanyUsers { get; set; }

        //public virtual ICollection<Branch> Branches { get; set; }

        /* 
         Settings:

         - Hide Industry Sector on Initial Screen : The system uses this industry sector 
         when you create a material. This facility is useful for those users who operate in 
         a single industry sector
          */

    }
}
