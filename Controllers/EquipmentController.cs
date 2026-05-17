using Lab_System.Data;
using Lab_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab_System.Controllers
{
    [Authorize]
    public class EquipmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipmentController(ApplicationDbContext context)
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
        public async Task<IActionResult> Create(Equipment equipment)
        {
            if (!ModelState.IsValid)
                return View(equipment);

            _context.Equipment.Add(equipment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard", new { tab = "equipment" });
        }

        [Authorize(Roles = "Admin,LabManager")]
        public async Task<IActionResult> Edit(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);

            if (equipment == null)
                return NotFound();

            return View(equipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,LabManager")]
        public async Task<IActionResult> Edit(Equipment equipment)
        {
            if (!ModelState.IsValid)
                return View(equipment);

            _context.Equipment.Update(equipment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard", new { tab = "equipment" });
        }

        [Authorize(Roles = "Admin,LabManager")]
        public async Task<IActionResult> Delete(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);

            if (equipment == null)
                return NotFound();

            return View(equipment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,LabManager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);

            if (equipment != null)
            {
                _context.Equipment.Remove(equipment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Dashboard", new { tab = "equipment" });
        }
    }
}