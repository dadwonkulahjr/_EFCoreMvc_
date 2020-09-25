using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMvc.ViewModels
{
    public class RegisterModel
    {
        [Required, MaxLength(50, ErrorMessage ="Username cannot exceed 50 characters.")]
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password and confirm password must match.")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
