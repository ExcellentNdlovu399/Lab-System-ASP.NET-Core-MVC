using System.ComponentModel.DataAnnotations;

namespace Lab_System.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Type { get; set; }

        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public string Status { get; set; }

        public DateTime LastCalibrated { get; set; }

        public int HoursUsed { get; set; }

        public string Location { get; set; }

        public string Notes { get; set; }
    }
}
