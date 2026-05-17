using System.ComponentModel.DataAnnotations;

namespace Lab_System.Models
{
    public class ExperimentChemical
    {
        public int Id { get; set; }

        public int ExperimentId { get; set; }
        public Experiment Experiment { get; set; }

        public int ChemicalId { get; set; }
        public Chemicals Chemical { get; set; }

        public double QuantityUsed { get; set; }

        public string Unit { get; set; }
    }
}