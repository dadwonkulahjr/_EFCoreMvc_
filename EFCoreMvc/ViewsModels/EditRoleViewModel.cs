using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace EFCoreMvc.ViewsModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        [Display(Name = "Role Id")]
        public string Id { get; set; }
        [Display(Name ="Role Name")]
        [Required(ErrorMessage ="Role name is required.")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
