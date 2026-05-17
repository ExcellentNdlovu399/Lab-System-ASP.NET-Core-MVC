using System.ComponentModel.DataAnnotations;

namespace Lab_System.Models
{
    public class ResearcherProfile
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser? User { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public string StudentNumber { get; set; }

        public string ResearchArea { get; set; }

        public string LabGroup { get; set; }

        public string Qualification { get; set; }

        public string PhoneNumber { get; set; }

        public string Biography { get; set; }

        public DateTime JoinedDate { get; set; } = DateTime.Now;
    }
}
