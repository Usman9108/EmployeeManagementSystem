using System.ComponentModel.DataAnnotations;

namespace CenterpriseAllProject.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
