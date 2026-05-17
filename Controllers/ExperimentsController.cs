using Lab_System.Data;
using Lab_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_System.Controllers
{
    [Authorize]
    public class ExperimentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExperimentsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin,Supervisor,Researcher")]
        public IActionResult Create()
        {
            ViewBag.Chemicals = _context.Chemicals.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Supervisor,Researcher")]
        public async Task<IActionResult> Create(
            Experiment experiment,
            int[] chemicalIds,
            double[] quantities,
            string[] units)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Chemicals = _context.Chemicals.ToList();
                return View(experiment);
            }

            var user = await _userManager.GetUserAsync(User);
            experiment.ResearcherId = user.Id;

            _context.Experiments.Add(experiment);
            await _context.SaveChangesAsync();

            for (int i = 0; i < chemicalIds.Length; i++)
            {
                if (chemicalIds[i] > 0 && quantities[i] > 0)
                {
                    _context.ExperimentChemicals.Add(new ExperimentChemical
                    {
                        ExperimentId = experiment.Id,
                        ChemicalId = chemicalIds[i],
                        QuantityUsed = quantities[i],
                        Unit = units[i]
                    });
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard", new { tab = "experiments" });
        }

        [Authorize(Roles = "Admin,Supervisor,Researcher")]
        public async Task<IActionResult> Edit(int id)
        {
            var experiment = await _context.Experiments
                .Include(e => e.ExperimentChemicals)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (experiment == null)
                return NotFound();

            ViewBag.Chemicals = await _context.Chemicals.ToListAsync();

            return View(experiment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Supervisor,Researcher")]
        public async Task<IActionResult> Edit(
            Experiment experiment,
            int[] chemicalIds,
            double[] quantities,
            string[] units)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Chemicals = await _context.Chemicals.ToListAsync();
                return View(experiment);
            }

            var existing = await _context.Experiments
                .Include(e => e.ExperimentChemicals)
                .FirstOrDefaultAsync(e => e.Id == experiment.Id);

            if (existing == null)
                return NotFound();

            existing.Title = experiment.Title;
            existing.Description = experiment.Description;
            existing.StartedDate = experiment.StartedDate;
            existing.Status = experiment.Status;
            existing.YieldPercentage = experiment.YieldPercentage;
            existing.Result = experiment.Result;

            _context.ExperimentChemicals.RemoveRange(existing.ExperimentChemicals);

            for (int i = 0; i < chemicalIds.Length; i++)
            {
                if (chemicalIds[i] > 0 && quantities[i] > 0)
                {
                    _context.ExperimentChemicals.Add(new ExperimentChemical
                    {
                        ExperimentId = existing.Id,
                        ChemicalId = chemicalIds[i],
                        QuantityUsed = quantities[i],
                        Unit = units[i]
                    });
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard", new { tab = "experiments" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var experiment = await _context.Experiments
                .Include(e => e.Researcher)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (experiment == null)
                return NotFound();

            return View(experiment);
        }

        [Authorize(Roles = "Admin,Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(int id, string status, string comment)
        {
            var experiment = await _context.Experiments.FindAsync(id);

            if (experiment == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);

            experiment.ApprovalStatus = status;
            experiment.SupervisorComment = comment;
            experiment.ApprovedById = user?.Id;
            experiment.ApprovedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var experiment = await _context.Experiments
                .Include(e => e.Researcher)
                .Include(e => e.ApprovedBy)
                .Include(e => e.Attachments)
                .Include(e => e.ExperimentChemicals)
                    .ThenInclude(ec => ec.Chemical)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (experiment == null)
                return NotFound();

            return View(experiment);
        }

        [Authorize(Roles = "Admin,Supervisor,Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFile(int experimentId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return RedirectToAction(nameof(Details), new { id = experimentId });

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "experiments");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var attachment = new ExperimentAttachment
            {
                ExperimentId = experimentId,
                FileName = file.FileName,
                FilePath = "/uploads/experiments/" + fileName,
                FileType = file.ContentType
            };

            _context.ExperimentAttachments.Add(attachment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = experimentId });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experiment = await _context.Experiments
                .Include(e => e.ExperimentChemicals)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (experiment != null)
            {
                _context.ExperimentChemicals.RemoveRange(experiment.ExperimentChemicals);
                _context.Experiments.Remove(experiment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Dashboard", new { tab = "experiments" });
        }
    }
}
