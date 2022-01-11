using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Actual Password")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The field {0} must be at least {1} characters long.")]
        public string OldPassword { get; set; }

        [Display(Name = "Nueva Password")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The field {0} must be at least {1} characters long.")]
        public string NewPassword { get; set; }

        [Display(Name = "Password Confirmation")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The field {0} must be at least {1} characters long.")]
        [Compare("NewPassword", ErrorMessage = "The new password and the confirmation are not the same.")]
        public string Confirm { get; set; }
    }
}
