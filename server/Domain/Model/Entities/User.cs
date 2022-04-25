using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Domain.Model.Entities
{
    public class User : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "用户名")]
        [Required]
        [StringLength(64)]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [Required]
        public byte[] PasswordHash { get; set; }

        public IList<UserRole>? UserRoles { get; set; }

    }
}
