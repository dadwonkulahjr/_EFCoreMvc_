using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMvc.Models
{
    public class AppUser : IdentityUser
    {

        [Required]
        [Column(name: "Gender", TypeName = "nvarchar(20)")]
        public Gender? Gender { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(50)")]
        public string Address { get; set; }
    }
}
