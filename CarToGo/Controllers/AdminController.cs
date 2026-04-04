using CarToGo.Models;
using CarToGo.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CarToGo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<DefaultUser> _userManager;
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<DefaultUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult ListAllRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListAllRoles");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewData["ErrorMessage"] = $"Role with Id = '{id}' cannot be found";
                return View("Error");
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewData["ErrorMessage"] = $"No role with Id '{model.Id}' was found";
                return View("Error");
            }
            else
            {
                role.Name = model.RoleName;

                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListAllRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            ViewData["roleId"] = id;
            ViewData["roleName"] = role.Name;

            if (role == null)
            {
                ViewData["ErrorMessage"] = $"No role with Id '{id}' was found";
                return View("Error");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users.ToList())
            {
                UserRoleViewModel userRoleVM = new()
                {
                    Id = user.Id,
                    Name = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleVM.IsSelected = true;
                }
                else
                {
                    userRoleVM.IsSelected = false;
                }

                model.Add(userRoleVM);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewData["ErrorMessage"] = $"No role with Id '{id}' was found";
                return View("Error");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].Id);

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
            }

            return RedirectToAction("EditRole", new { Id = id });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewData["ErrorMessage"] = $"No role with Id '{id}' was found";
                return View("Error");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListAllRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(role);
            }
        }

        public async Task<IActionResult> ListAllUsers()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(DefaultUser model, string password)
        {
            if(ModelState.IsValid)
    {
                // Username = Email
                model.UserName = model.Email;

                // Check unique Email
                if (await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    ModelState.AddModelError("Email", "This email is already in use.");
                    return View(model);
                }

                // Check unique Username
                if (await _userManager.FindByNameAsync(model.UserName) != null)
                {
                    ModelState.AddModelError("UserName", "This username is already in use.");
                    return View(model);
                }

                // Check unique EGN
                if (_userManager.Users.Any(u => u.EGN == model.EGN))
                {
                    ModelState.AddModelError("EGN", "This EGN is already in use.");

                    return View(model);
                }

                model.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(model, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(model, "User");
                    return RedirectToAction("ListAllUsers");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(DefaultUser model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            user.Email = model.Email;
            user.UserName = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.EGN = model.EGN;
            user.PhoneNumber = model.PhoneNumber;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("ListAllUsers");
        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);

            return RedirectToAction("ListAllUsers");
        }

        public async Task<IActionResult> DetailsUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

    }
}
