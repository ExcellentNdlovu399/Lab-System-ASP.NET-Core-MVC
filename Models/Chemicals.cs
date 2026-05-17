using System.ComponentModel.DataAnnotations;

namespace Lab_System.Models
{
    public class Chemicals
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Formula { get; set; }

        public string State { get; set; }

        public double MolecularWeight { get; set; }

        public string CasNumber { get; set; }

        public string StockQuantity { get; set; }

        public string HazardClass { get; set; }

        public string Supplier { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string StorageLocation { get; set; }

        public string Purity { get; set; }

        public string Unit { get; set; }

        public string Notes { get; set; }

        public ICollection<ExperimentChemical> ExperimentChemicals { get; set; }
    = new List<ExperimentChemical>();
    }
}
