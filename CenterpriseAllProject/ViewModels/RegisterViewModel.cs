using CenterpriseAllProject.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CenterpriseAllProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote("IsEmailInUse","Account")]
        [ValidEmailDomain(allowedDomain: "pragimtech.com",ErrorMessage = "Email domain must be pragimtech.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",
            ErrorMessage ="Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string? City { get; set; }
    }
}
