using System.ComponentModel.DataAnnotations;

namespace Lab_System.Models
{
    public class Experiment
    {
        public int Id { get; set; }

        public int ExperimentNumber { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartedDate { get; set; }

        public string Status { get; set; }

        public double YieldPercentage { get; set; }

        public string Result { get; set; }

        public string? ResearcherId { get; set; }

        public ApplicationUser? Researcher { get; set; }

        public string Theory { get; set; }

        public string Procedure { get; set; }

        public string ExperimentalData { get; set; }

        public string Calculations { get; set; }

        public string FinalResults { get; set; }

        public string ApprovalStatus { get; set; } = "pending";

        public string? SupervisorComment { get; set; }

        public string? ApprovedById { get; set; }

        public ApplicationUser? ApprovedBy { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public ICollection<ExperimentAttachment> Attachments { get; set; }
    = new List<ExperimentAttachment>();
        public ICollection<ExperimentChemical> ExperimentChemicals { get; set; }
            = new List<ExperimentChemical>();
    }
}