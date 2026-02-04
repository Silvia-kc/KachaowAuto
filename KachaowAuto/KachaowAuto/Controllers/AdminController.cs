using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Users()
        {
            var allRoles = _roleManager.Roles.Select(r => r.Name!).ToList();
            var users = _userManager.Users.ToList();

            var vm = new List<AdminUserViewModel>();
            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                vm.Add(new AdminUserViewModel
                {
                    UserId = u.Id,
                    Email = u.Email ?? "",
                    FullName = u.FullName ?? "",
                    CurrentRole = roles.FirstOrDefault() ?? "",
                    AllRoles = allRoles
                });
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetRole(int userId, string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                return RedirectToAction(nameof(Users));

            if (!await _roleManager.RoleExistsAsync(role))
                return BadRequest("Role does not exist.");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return NotFound();

            var currentUserId = int.Parse(_userManager.GetUserId(User)!);
            if (user.Id == currentUserId && role != "Admin")
            { 

            return BadRequest("You cannot remove your own Admin role.");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, role);

            return RedirectToAction(nameof(Users));
        }
    }
}
