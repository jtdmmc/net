using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Domain.Model.Entities
{
    public class RolePower
    {
        [Display(Name = "权限ID")]
        [Required]
        public int PowerId { get; set; }

        [Display(Name = "角色ID")]
        [Required]
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public Power Power { get; set; }
    }
}
