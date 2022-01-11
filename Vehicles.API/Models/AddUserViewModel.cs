using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models
{
    public class AddUserViewModel : EditUserViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "You must enter a valid email.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(6, ErrorMessage = "The field {0} must be at least {1} characters long.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Password { get; set; }

        [Display(Name = "Password Confirmation")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The field {0} must be at least {1} characters long.")]
        [Compare("Password", ErrorMessage = "The Password and Password confirmation are not the same.")]
        public string PasswordConfirm { get; set; }
    }
}
