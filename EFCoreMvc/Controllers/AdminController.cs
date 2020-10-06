using EFCoreMvc.AccountModels;
using EFCoreMvc.Models;
using EFCoreMvc.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EFCoreMvc.Controllers
{
    [Authorize(Policy = "AdminRolePolicy")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager,
            ILogger<AdminController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole(createRoleViewModel.RoleName);
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Admin");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(createRoleViewModel);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var identityRole = _roleManager.Roles;
            return View(identityRole);
        }

        [HttpGet]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(roleId: id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found.";
                return RedirectToAction("NotFound");
            }
            var model = new EditRoleViewModel()
            {
                RoleName = role.Name,
                Id = role.Id
            };

            List<AppUser> users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(roleId: model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found.";
                return RedirectToAction("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, errors.Description);
                }

                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId: roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found.";
                return RedirectToAction("NotFound");
            }

            var model = new List<UserRoleViewModel>();
            List<AppUser> users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                UserRoleViewModel userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    Username = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId: roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found.";
                return RedirectToAction("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                AppUser user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult identityResult = null;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    identityResult = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    identityResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else { continue; }

                if (identityResult.Succeeded)
                {
                    if (i < model.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }

        //Display All Users
        [HttpGet]
        public IActionResult ListUsers()
        {
            var getAllUsers = _userManager.Users.ToList();
            return View(getAllUsers);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser identityUser = new AppUser()
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Gender = model.Gender,
                    Address = model.Address
                };
                //Perform magic here!
                var result = await _userManager.CreateAsync(identityUser, model.Password);
                if (result.Succeeded)
                {
                    //Perform magic here!
                    //await _signInManager.SignInAsync(identityUser, isPersistent: false);
                    return RedirectToAction("ListUsers");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditAspUser(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found.";
                return RedirectToAction("NotFound");
            }

            var userCliams = await _userManager.GetClaimsAsync(appUser);
            var userRoles = await _userManager.GetRolesAsync(appUser);

            var model = new EditUserViewModel()
            {
                Id = appUser.Id,
                Username = appUser.UserName,
                Email = appUser.Email,
                Gender = appUser.Gender,
                Address = appUser.Address,
                Cliams = userCliams.Select(c => c.Type + ": " + c.Value).ToList(),
                Roles = userRoles.ToList()

            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditAspUser(EditUserViewModel updateUser)
        {
            AppUser appUser = await _userManager.FindByIdAsync(updateUser.Id);
            if (appUser == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {updateUser.Id} cannot be found.";
                return RedirectToAction("NotFound");
            }
            else
            {
                appUser.UserName = updateUser.Username;
                appUser.Email = updateUser.Email;
                appUser.Gender = updateUser.Gender;
                appUser.Address = updateUser.Address;

                var result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, errors.Description);
                }

                return View(appUser);
            }

        }

        [HttpPost]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(roleId: id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found.";
                return RedirectToAction("NotFound");
            }
            else
            {
                try
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, errorMessage: error.Description);
                    }
                    return View(role);
                }
                catch (DbUpdateException ex)
                {
                    ViewBag.ErrorTitle = $"{role.Name} role is in use";
                    ViewBag.ErrorMessage = $"{role.Name} role cannot be deleted as there are users in this role. " +
                    "If you want to delete this role, please remove the users from the role " +
                    "and then try to delete it again...";
                    _logger.LogError(ex.Message);

                    return View("Error");
                }
            }

        }

        [HttpGet]
        public async Task<IActionResult> ManageUsersInRole(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId: userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found.";
                return RedirectToAction("NotFound");
            }

            List<UserRolesViewModel> model = new List<UserRolesViewModel>();
            List<IdentityRole> roles = _roleManager.Roles.ToList();
            foreach (var role in roles)
            {
                UserRolesViewModel userRolesViewModel = new UserRolesViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUsersInRole(List<UserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId: userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found.";
                return RedirectToAction("NotFound");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot remove user existing roles.");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(x => x.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot add selected roles to user.");
                return View(model);
            }

            return RedirectToAction("EditAspUser", new { Id = userId });
        }


        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found.";
                return RedirectToAction("NotFound");
            }
            var existingClaims = await _userManager.GetClaimsAsync(user);

            var model = new UserClaimViewModel()
            {
                UserId = userId
            };

            foreach (Claim claim in ClaimsStore.ListOfUserClaims)
            {
                UserClaims userClaims = new UserClaims()
                {
                    ClaimType = claim.Type
                };

                if(existingClaims.Any(x => x.Type == claim.Type && x.Value == "true"))
                {
                    userClaims.IsSelected = true;
                }

                model.UserClaims.Add(userClaims);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.UserId} cannot be found.";
                return RedirectToAction("NotFound");
            }
            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot remove user existing claims.");
                return View(model);
            }

            result = await _userManager.AddClaimsAsync(user,
                model.UserClaims.Select(x => new Claim(x.ClaimType, x.IsSelected ? "true" : "false")));

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot add selected claims to user.");
                return View(model);
            }

            return RedirectToAction("EditAspUser", new { Id = model.UserId });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
