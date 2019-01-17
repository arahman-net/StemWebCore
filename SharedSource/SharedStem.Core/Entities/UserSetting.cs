
using Stem.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("UserSetting", Schema = "Core")]
    public class UserSetting : BaseEntity
    {
        public string Module { get; set; }
        public string Entity { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }

        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        public int SysUserId { get; set; }
        public SysUser SysUser { get; set; }

        //public int SysUserId { get; set; }
        //public virtual SysUser SysUser { get; set; }

    }
}
