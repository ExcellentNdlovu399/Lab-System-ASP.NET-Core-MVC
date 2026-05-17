using Lab_System.Models;
using Lab_System.Models;

namespace Lab_System.Models.ViewModels
{
    public class DashboardViewModel
    {
        public string ActiveTab { get; set; } = "overview";
        public string? SearchTerm { get; set; }

        public int ChemicalCount { get; set; }
        public int EquipmentCount { get; set; }
        public int ActiveExperimentCount { get; set; }
        public double LatestYield { get; set; }

        public int InProgressCount { get; set; }
        public int PlannedCount { get; set; }
        public int CompletedCount { get; set; }

        public int PendingApprovalCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int RevisionRequiredCount { get; set; }

        public List<Chemicals> Chemicals { get; set; } = new();
        public List<Equipment> Equipment { get; set; } = new();
        public List<Experiment> Experiments { get; set; } = new();
        public List<ReactionStep> Reactions { get; set; } = new();
        public List<ResearcherProfile> Researchers { get; set; } = new();

        public List<ChemicalAlertViewModel> ExpiryAlerts { get; set; } = new();
        public List<ActivityViewModel> RecentActivities { get; set; } = new();

        public List<ChemicalUsageViewModel> ChemicalUsage { get; set; } = new();

        public bool IsActive(string tab)
        {
            return ActiveTab == tab;
        }
    }

    public class ChemicalAlertViewModel
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int DaysLeft { get; set; }
    }

    public class ActivityViewModel
    {
        public string Title { get; set; }
        public string Meta { get; set; }
    }
}