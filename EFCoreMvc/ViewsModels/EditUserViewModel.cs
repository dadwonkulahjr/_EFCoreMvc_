using EFCoreMvc.Models;
using EFCoreMvc.tuseTheProgrammerCustomUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EFCoreMvc.ViewsModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Cliams = new List<string>();
            Roles = new List<string>();
        }
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
     
        [Required, RegularExpression(pattern: @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address.")]
        [Display(Name = "Office Email")]
        [tuseTheProgrammerCustomEmailValidatorAtttribute(requiredEmailDomain: "tuseTheProgrammer.com", ErrorMessage = "The required Email domain must only be tuseTheProgrammer.com")]
        public string Email { get; set; }
        [Required]
        public Gender? Gender { get; set; }
        [Required]
        public string Address { get; set; }
        public List<string> Cliams { get; set; }
        public List<string> Roles { get; set; }
    }
}
