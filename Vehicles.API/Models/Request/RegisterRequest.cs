using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models.Request
{
    public class RegisterRequest : UserRequest
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(6, ErrorMessage = "The field {0} must be at least {1} characters long.")]
        public string Password { get; set; }
    }
}
