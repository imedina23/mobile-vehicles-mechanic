using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = "You must enter a valid email.")]
        public string UserName { get; set; }

        [Display(Name = "Nueva Password")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(6, ErrorMessage = "The field {0} must be at least {1} characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Password Confirmation")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(6, ErrorMessage = "The field {0} must be at least {1} characters long.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The new password and the confirmation are not the same.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
