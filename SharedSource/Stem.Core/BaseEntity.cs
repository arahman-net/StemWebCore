using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stem.Core
{
    public abstract partial class BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? CreatedBy { get; set; }
        public System.DateTime? CreationDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public System.DateTime? LastModificationDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
