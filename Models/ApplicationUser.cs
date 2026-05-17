using Microsoft.AspNetCore.Identity;

namespace Lab_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string RoleName {  get; set; }
    }
}
