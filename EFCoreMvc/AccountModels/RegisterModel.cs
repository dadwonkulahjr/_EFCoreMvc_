using EFCoreMvc.Models;
using EFCoreMvc.tuseTheProgrammerCustomUtilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMvc.AccountModels
{
    public class RegisterModel
    {
        [Required, StringLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
        [Remote(action: "IsUsernameInUse", controller: "Account")]
        public string Username { get; set; }
        [EmailAddress]
        [Required]
        [Display(Name = "Office Email")]
        [DataType(DataType.EmailAddress)]
        [tuseTheProgrammerCustomEmailValidatorAtttribute(requiredEmailDomain: "tuseTheProgrammer.com", ErrorMessage ="The required Email domain must only be tuseTheProgrammer.com")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public Gender? Gender { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
