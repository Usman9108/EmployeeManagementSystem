using Microsoft.AspNetCore.Identity;

namespace CenterpriseAllProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? City { get; set; }
    }
}
