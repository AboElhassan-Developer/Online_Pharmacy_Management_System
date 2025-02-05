using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Pharmacy_Management_System.Models;
using Online_Pharmacy_Management_System.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Pharmacy_Management_System.Controllers
{
    [Authorize(Roles = "Admin")] // Admin فقط يمكنه إدارة الأدوار
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRoles = new Dictionary<string, string>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roles.FirstOrDefault() ?? "No Role";
            }

            // إرسال الـ users فقط إلى الـ View المناسب
            ViewBag.UserRoles = userRoles;
            return View(users); // هنا نمرر الـ List<ApplicationUser> بدلاً من List<IdentityRole>
        }

        public async Task<IActionResult> ManageRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles); // هنا نمرر الـ List<IdentityRole> بدلاً من List<ApplicationUser>
        }





        public async Task<IActionResult> EditUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles,
                CurrentRole = userRoles.FirstOrDefault() ?? "No Role"
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserRole(EditUserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            if (string.IsNullOrEmpty(model.SelectedRole))
            {
                ModelState.AddModelError("", "Please select a valid role.");
                return View(model);
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, model.SelectedRole);

            TempData["SuccessMessage"] = "User role updated successfully!";
            return RedirectToAction("ManageUsers");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);
            TempData["SuccessMessage"] = "User deleted successfully!";
            return RedirectToAction("ManageUsers");
        }
    }
}
