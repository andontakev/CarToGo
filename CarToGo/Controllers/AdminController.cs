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
        /// <summary>
        /// AdminController constructor that initializes the RoleManager and UserManager for managing roles and users in the application.
        /// </summary>
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<DefaultUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        /// <summary>
        /// Lists all roles in the system by retrieving them from the RoleManager and passing them to the view for display.
        /// </summary>
        [HttpGet]
        public IActionResult ListAllRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        /// <summary>
        /// Returns the view for adding a new role.
        /// </summary>
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        /// <summary>
        /// Handles HTTP POST requests to create a new role using the specified view model.
        /// </summary>
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
        /// <summary>
        /// Displays the edit view for a specified role, allowing modification of the role's details and viewing of
        /// associated users.
        /// </summary>
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
        /// <summary>
        /// Updates the name of an existing role based on the provided view model.
        /// </summary>
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
        /// <summary>
        /// Updates the users assigned to a specific role based on the provided list of user-role view models
        /// </summary>
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
        /// <summary>
        /// Updates the users assigned to a specific role based on the provided list of user-role view models
        /// </summary>
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
        /// <summary>
        /// Deletes a specified role from the system after confirming the action with the user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return View(role);
        }
        /// <summary>
        /// Confirms the deletion of a specified role and removes it from the system if the user confirms the action.
        /// </summary>
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
        /// <summary>
        /// Lists all users in the system by retrieving them from the UserManager and passing them to the view for display.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ListAllUsers()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        /// <summary>
        /// Creates a new user in the system based on the provided view model and password
        /// <returns></returns>
        public IActionResult CreateUser()
        {
            return View();
        }
        /// <summary>
        /// Creates a new user in the system based on the provided view model and password
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUser(DefaultUser model, string password)
        {
            if(ModelState.IsValid)
    {
                // Username = Email
                model.UserName = model.Email;

                
                if (await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    ModelState.AddModelError("Email", "This email is already in use.");
                    return View(model);
                }

                if (await _userManager.FindByNameAsync(model.UserName) != null)
                {
                    ModelState.AddModelError("UserName", "This username is already in use.");
                    return View(model);
                }

                
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
        /// <summary>
        /// Edits the details of an existing user in the system based on the provided view model and updates the user's information accordingly.
        /// </summary>
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }
        /// <summary>
        /// Edits the details of an existing user in the system based on the provided view model and updates the user's information accordingly.
        /// </summary>
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
        /// <summary>
        /// Deletes a specified user from the system after confirming the action with the user.
        /// </summary>
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }
        /// <summary>
        /// Deletes a specified user from the system after confirming the action with the user.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);

            return RedirectToAction("ListAllUsers");
        }
        /// <summary>
        /// Details the information of a specified user by retrieving their details from the UserManager and passing them to the view for display.
        /// </summary>
        public async Task<IActionResult> DetailsUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

    }
}
