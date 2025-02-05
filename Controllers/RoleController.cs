using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Pharmacy_Management_System.Models;
using Online_Pharmacy_Management_System.ViewModel;

namespace Online_Pharmacy_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleViewModel.RoleName);
                if (!roleExists)
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(roleViewModel.RoleName));
                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "Role created successfully!";
                        return RedirectToAction("ManageRoles");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to create role.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Role already exists.");
                }
            }
            return View("AddRole", roleViewModel);
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var model = new RoleViewModel { RoleName = role.Name };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string id, RoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            role.Name = model.RoleName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Role updated successfully!";
                return RedirectToAction("ManageRoles");
            }

            ModelState.AddModelError("", "Failed to update role.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return NotFound();

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            if (usersInRole.Any())
            {
                TempData["ErrorMessage"] = "Cannot delete role. Users are assigned to this role.";
                return RedirectToAction("ManageRoles");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Role deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete role.";
            }

            return RedirectToAction("ManageRoles");
        }
    }
}