using Lab_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Lab_System.Models
{
    public class ReactionStep
    {
        public int Id { get; set; }

        [Required]
        public string StepTitle { get; set; }

        public string Description { get; set; }

        public double Temperature { get; set; }

        public string Pressure { get; set; }

        public string ReactionEquation { get; set; }

        public string StepType { get; set; }

        public int StepOrder { get; set; }

        public int ExperimentId { get; set; }

        public Experiment? Experiment { get; set; }

        public int? ParentStepId { get; set; }

        public ReactionStep? ParentStep { get; set; }

        public ICollection<ReactionStep> ChildSteps { get; set; }
            = new List<ReactionStep>();
    }
}