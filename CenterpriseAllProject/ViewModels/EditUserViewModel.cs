using CenterpriseAllProject.Models;
using System.ComponentModel.DataAnnotations;

namespace CenterpriseAllProject.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public string City { get; set; }
        public IList<string> Roles { get; set; }
        public List<string> UserClaims { get; set; }

        public EditUserViewModel()
        {
            Roles = new List<string>();
            UserClaims = new List<string>();
        }
    }
}
