using SharedStem.Core.Entities;
using Stem.Core;

namespace SharedStem.Core
{
    public class CompanyEntity : BaseEntity
    {
        public int CompanyContextId { get; set; }
        public virtual Company CompanyContext { get; set; }
    }
}
