using Lab_System.Data;
using Lab_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_System.Controllers
{
    [Authorize]
    public class ResearchersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ResearchersController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var profiles = await _context.ResearcherProfiles
                .Include(r => r.User)
                .ToListAsync();

            return View(profiles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var profile = await _context.ResearcherProfiles
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (profile == null)
                return NotFound();

            return View(profile);
        }

        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = await _userManager.Users.ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Create(ResearcherProfile profile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = await _userManager.Users.ToListAsync();
                return View(profile);
            }

            _context.ResearcherProfiles.Add(profile);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Edit(int id)
        {
            var profile = await _context.ResearcherProfiles.FindAsync(id);

            if (profile == null)
                return NotFound();

            ViewBag.Users = await _userManager.Users.ToListAsync();

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Edit(ResearcherProfile profile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = await _userManager.Users.ToListAsync();
                return View(profile);
            }

            _context.ResearcherProfiles.Update(profile);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _context.ResearcherProfiles
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (profile == null)
                return NotFound();

            return View(profile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.ResearcherProfiles.FindAsync(id);

            if (profile != null)
            {
                _context.ResearcherProfiles.Remove(profile);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
