using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMvc.AccountModels
{
    public class LoginModel
    {
        [Required, StringLength(50, ErrorMessage = "Name cannot exceed 20 characters.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }
}
