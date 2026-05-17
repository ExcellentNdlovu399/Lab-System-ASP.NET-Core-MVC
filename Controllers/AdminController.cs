using Lab_System.Models;
using Lab_System.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_System.Controllers
{
    [Authorize(Roles = "Admin,Supervisor")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        [Authorize(Roles = "Admin,Supervisor")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            if (User.IsInRole("Supervisor") && user.RoleName != "Researcher")
                return Forbid();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.RoleName
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (User.IsInRole("Supervisor") && model.Role != "Researcher")
            {
                ModelState.AddModelError("", "Supervisor can only manage Researcher accounts.");
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return NotFound();

            if (User.IsInRole("Supervisor") && user.RoleName != "Researcher")
                return Forbid();

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.RoleName = model.Role;

            var oldRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, oldRoles);
            await _userManager.AddToRoleAsync(user, model.Role);

            await _userManager.UpdateAsync(user);

            if (User.IsInRole("Admin"))
                return RedirectToAction(nameof(Users));

            return RedirectToAction("Index", "Dashboard");
        }

        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            if (User.IsInRole("Supervisor") && user.RoleName != "Researcher")
                return Forbid();

            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            if (User.IsInRole("Supervisor") && user.RoleName != "Researcher")
                return Forbid();

            await _userManager.DeleteAsync(user);

            if (User.IsInRole("Admin"))
                return RedirectToAction(nameof(Users));

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (User.IsInRole("Supervisor") && model.Role != "Researcher")
            {
                ModelState.AddModelError("", "Supervisor can only create Researcher accounts.");
                return View(model);
            }

            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                FullName = model.FullName,
                UserName = model.Email,
                Email = model.Email,
                RoleName = model.Role,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);

                if (User.IsInRole("Admin"))
                    return RedirectToAction(nameof(Users));

                return RedirectToAction("Index", "Dashboard");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }
    }
}