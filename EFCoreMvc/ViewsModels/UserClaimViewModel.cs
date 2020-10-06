using EFCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMvc.ViewsModels
{
    public class UserClaimViewModel
    {
        public UserClaimViewModel()
        {
            UserClaims = new List<UserClaims>();
        }
        public string UserId { get; set; }
        public List<UserClaims> UserClaims { get; set; }
    }
}
