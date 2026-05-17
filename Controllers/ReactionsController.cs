using Lab_System.Data;
using Lab_System.Models;
using Lab_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_System.Controllers
{
    [Authorize]
    public class ReactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,Researcher")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Experiments = await _context.Experiments.ToListAsync();
            ViewBag.ParentSteps = await _context.ReactionSteps.ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Researcher")]
        public async Task<IActionResult> Create(ReactionStep reactionStep)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Experiments = await _context.Experiments.ToListAsync();
                ViewBag.ParentSteps = await _context.ReactionSteps.ToListAsync();

                return View(reactionStep);
            }

            _context.ReactionSteps.Add(reactionStep);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard", new { tab = "reactions" });
        }

        [Authorize(Roles = "Admin,Researcher")]
        public async Task<IActionResult> Edit(int id)
        {
            var reactionStep = await _context.ReactionSteps.FindAsync(id);

            if (reactionStep == null)
                return NotFound();

            ViewBag.Experiments = await _context.Experiments.ToListAsync();

            ViewBag.ParentSteps = await _context.ReactionSteps
                .Where(r => r.Id != id)
                .ToListAsync();

            return View(reactionStep);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Researcher")]
        public async Task<IActionResult> Edit(ReactionStep reactionStep)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Experiments = await _context.Experiments.ToListAsync();

                ViewBag.ParentSteps = await _context.ReactionSteps
                    .Where(r => r.Id != reactionStep.Id)
                    .ToListAsync();

                return View(reactionStep);
            }

            _context.ReactionSteps.Update(reactionStep);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard", new { tab = "reactions" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var reactionStep = await _context.ReactionSteps
                .Include(r => r.Experiment)
                .Include(r => r.ParentStep)
                .Include(r => r.ChildSteps)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reactionStep == null)
                return NotFound();

            return View(reactionStep);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var reactionStep = await _context.ReactionSteps
                .Include(r => r.Experiment)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reactionStep == null)
                return NotFound();

            return View(reactionStep);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reactionStep = await _context.ReactionSteps.FindAsync(id);

            if (reactionStep != null)
            {
                _context.ReactionSteps.Remove(reactionStep);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Dashboard", new { tab = "reactions" });
        }
    }
}