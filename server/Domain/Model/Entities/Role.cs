using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Domain.Model.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "角色名称")]
        [Required]
        [StringLength(64)]
        public string RoleName { get; set; }

        public IList<RolePower>? RolePowers { get; set; }
    }
}
