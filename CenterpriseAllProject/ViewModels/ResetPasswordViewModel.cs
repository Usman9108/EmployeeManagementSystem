using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace CenterpriseAllProject.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage ="Password and confirm Password must match")]
        public string ConfirmPassword { get; set;}
        public string Token { get; set; }
    }
}
