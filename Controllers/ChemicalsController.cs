using Lab_System.Data;
using Lab_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_System.Controllers
{
    [Authorize]
    public class ChemicalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChemicalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,LabManager")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,LabManager")]
        public async Task<IActionResult> Create(Chemicals chemical)
        {
            if (!ModelState.IsValid)
                return View(chemical);

            _context.Chemicals.Add(chemical);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard", new { tab = "chemicals" });
        }

        [Authorize(Roles = "Admin,LabManager")]
        public async Task<IActionResult> Edit(int id)
        {
            var chemical = await _context.Chemicals.FindAsync(id);

            if (chemical == null)
                return NotFound();

            return View(chemical);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,LabManager")]
        public async Task<IActionResult> Edit(Chemicals chemical)
        {
            if (!ModelState.IsValid)
                return View(chemical);

            _context.Chemicals.Update(chemical);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard", new { tab = "chemicals" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var chemical = await _context.Chemicals.FindAsync(id);

            if (chemical == null)
                return NotFound();

            return View(chemical);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chemical = await _context.Chemicals.FindAsync(id);

            if (chemical != null)
            {
                _context.Chemicals.Remove(chemical);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Dashboard", new { tab = "chemicals" });
        }
    }
}