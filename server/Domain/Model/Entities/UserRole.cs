using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Domain.Model.Entities
{
    public class UserRole
    {

        [Display(Name = "用户ID")]
        [Required]
        public int UserId { get; set; }

        [Display(Name = "角色ID")]
        [Required]
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public User User { get; set; }
    }
}
