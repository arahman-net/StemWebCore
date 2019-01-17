using Stem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StemWeb.Core.Models.ViewModels
{
    public class ModuleFeaturesModel : BaseEntity
    {
        public int AppModuleId { get; set; }
        public string GroupName { get; set; }
        public string SubGroupName { get; set; }

        public IEnumerable<int> ModuleFeatures { get; set; }
    }
}
