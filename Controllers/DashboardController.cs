using Lab_System.Data;
using Lab_System.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_System.Controllers
{
   // [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string tab = "overview", string? search = null)
        {
            tab = string.IsNullOrWhiteSpace(tab)
                ? "overview"
                : tab.ToLower();

            var latestExperiment = await _context.Experiments
                .OrderByDescending(e => e.StartedDate)
                .FirstOrDefaultAsync();

            var chemicals = await _context.Chemicals.ToListAsync();
            var equipment = await _context.Equipment.ToListAsync();
            var experiments = await _context.Experiments
                .Include(e => e.Researcher)
                .ToListAsync();

            var reactions = await _context.ReactionSteps
                .Include(r => r.Experiment)
                .ToListAsync();

            var researchers = await _context.ResearcherProfiles
                .Include(r => r.User)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var q = search.ToLower();

                chemicals = chemicals
                    .Where(c =>
                        (c.Name ?? "").ToLower().Contains(q) ||
                        (c.Formula ?? "").ToLower().Contains(q) ||
                        (c.State ?? "").ToLower().Contains(q) ||
                        (c.HazardClass ?? "").ToLower().Contains(q) ||
                        (c.Supplier ?? "").ToLower().Contains(q) ||
                        (c.CasNumber ?? "").ToLower().Contains(q) ||
                        (c.StorageLocation ?? "").ToLower().Contains(q) ||
                        (c.Purity ?? "").ToLower().Contains(q) ||
                        (c.Unit ?? "").ToLower().Contains(q) ||
                        (c.Notes ?? "").ToLower().Contains(q))
                    .ToList();

                equipment = equipment
                    .Where(e =>
                        (e.Name ?? "").ToLower().Contains(q) ||
                        (e.Type ?? "").ToLower().Contains(q) ||
                        (e.Model ?? "").ToLower().Contains(q) ||
                        (e.SerialNumber ?? "").ToLower().Contains(q) ||
                        (e.Status ?? "").ToLower().Contains(q) ||
                        (e.Location ?? "").ToLower().Contains(q) ||
                        (e.Notes ?? "").ToLower().Contains(q))
                    .ToList();

                experiments = experiments
                    .Where(e =>
                        (e.Title ?? "").ToLower().Contains(q) ||
                        (e.Description ?? "").ToLower().Contains(q) ||
                        (e.Status ?? "").ToLower().Contains(q) ||
                        (e.Researcher != null &&
                         (e.Researcher.FullName ?? "").ToLower().Contains(q)))
                    .ToList();

                reactions = reactions
                    .Where(r =>
                        (r.StepTitle ?? "").ToLower().Contains(q) ||
                        (r.Description ?? "").ToLower().Contains(q) ||
                        (r.Pressure ?? "").ToLower().Contains(q) ||
                        (r.Experiment != null &&
                         (r.Experiment.Title ?? "").ToLower().Contains(q)))
                    .ToList();

                researchers = researchers
                    .Where(r =>
                        (r.Department ?? "").ToLower().Contains(q) ||
                        (r.Position ?? "").ToLower().Contains(q) ||
                        (r.User != null &&
                         ((r.User.FullName ?? "").ToLower().Contains(q) ||
                          (r.User.Email ?? "").ToLower().Contains(q))))
                    .ToList();
            }

            var model = new DashboardViewModel
            {
                ActiveTab = tab,
                SearchTerm = search,

                ChemicalCount = await _context.Chemicals.CountAsync(),
                EquipmentCount = await _context.Equipment.CountAsync(),

                ActiveExperimentCount = await _context.Experiments
                    .CountAsync(e => e.Status != null &&
                                     e.Status.ToLower() == "in-progress"),

                LatestYield = latestExperiment != null
                    ? latestExperiment.YieldPercentage
                    : 0,

                InProgressCount = await _context.Experiments
                    .CountAsync(e => e.Status != null &&
                                     e.Status.ToLower() == "in-progress"),

                PlannedCount = await _context.Experiments
                    .CountAsync(e => e.Status != null &&
                                     e.Status.ToLower() == "planned"),

                CompletedCount = await _context.Experiments
                    .CountAsync(e => e.Status != null &&
                                     e.Status.ToLower() == "completed"),

                PendingApprovalCount = await _context.Experiments
    .CountAsync(e => e.ApprovalStatus == "pending"),

                ApprovedCount = await _context.Experiments
    .CountAsync(e => e.ApprovalStatus == "approved"),

                RejectedCount = await _context.Experiments
    .CountAsync(e => e.ApprovalStatus == "rejected"),

                RevisionRequiredCount = await _context.Experiments
    .CountAsync(e => e.ApprovalStatus == "revision-required"),

                Chemicals = chemicals,
                Equipment = equipment,
                Experiments = experiments,
                Reactions = reactions,
                Researchers = researchers
            };

            model.ExpiryAlerts = await _context.Chemicals
                .OrderBy(c => c.ExpiryDate)
                .Take(4)
                .Select(c => new ChemicalAlertViewModel
                {
                    Name = c.Name,
                    Quantity = c.StockQuantity,
                    ExpiryDate = c.ExpiryDate,
                    DaysLeft = EF.Functions.DateDiffDay(DateTime.Today, c.ExpiryDate)
                })
                .ToListAsync();

            model.RecentActivities.Add(new ActivityViewModel
            {
                Title = latestExperiment != null
                    ? $"Result recorded — {latestExperiment.YieldPercentage}% yield"
                    : "No experiment recorded yet",

                Meta = latestExperiment != null
                    ? $"{latestExperiment.Title} · {latestExperiment.StartedDate:MMM dd, yyyy}"
                    : "Create your first experiment"
            });

            model.ChemicalUsage = await _context.ExperimentChemicals
    .Include(x => x.Chemical)
    .GroupBy(x => new
    {
        x.Chemical.Name,
        x.Unit
    })
    .Select(g => new ChemicalUsageViewModel
    {
        Name = g.Key.Name,
        Amount = g.Sum(x => x.QuantityUsed) + " " + g.Key.Unit,
        Percentage = (int)Math.Min(g.Sum(x => x.QuantityUsed), 100)
    })
    .Take(5)
    .ToListAsync();

            ViewData["ActiveTab"] = model.ActiveTab;
            ViewData["SearchTerm"] = model.SearchTerm ?? "";

            ViewData["ChemicalCount"] = model.ChemicalCount;
            ViewData["EquipmentCount"] = model.EquipmentCount;
            ViewData["ExperimentCount"] = model.Experiments.Count;
            ViewData["ReactionCount"] = model.Reactions.Count;
            ViewData["ResearcherCount"] = model.Researchers.Count;

            return View(model);
        }
    }
}