using CenterpriseAllProject.Models;
using System.ComponentModel.DataAnnotations;

namespace CenterpriseAllProject.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 charaters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
