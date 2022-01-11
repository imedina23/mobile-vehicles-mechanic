using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models.Request
{
    public class UserRequest
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public string Email { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string LastName { get; set; }

        [MaxLength(20, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Document { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Address { get; set; }

        [MaxLength(5, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string CountryCode { get; set; }

        [MaxLength(20, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public int DocumentTypeId { get; set; }

        public byte[] Image { get; set; }
    }
}
