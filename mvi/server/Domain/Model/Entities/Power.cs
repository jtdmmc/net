using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Domain.Model.Entities
{
    public class Power
    {

        [Key]
        public int Id { get; set; }


        [Display(Name = "权限名称")]
        [Required]
        [StringLength(64)]
        public string PowerName { get; set; }

    }
}
