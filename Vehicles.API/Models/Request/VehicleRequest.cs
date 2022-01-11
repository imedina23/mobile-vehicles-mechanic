using System;
using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models.Request
{
    public class VehicleRequest
    {
        public int Id { get; set; }

        [Display(Name = "Vehicle Type")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int VehicleTypeId { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int BrandId { get; set; }

        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [Range(1900, 3000, ErrorMessage = "Invalid model value.")]
        public int Model { get; set; }

        [Display(Name = "Plate")]
        [RegularExpression(@"[a-zA-Z]{3}[0-9]{2}[a-zA-Z0-9]", ErrorMessage = "Incorrect plate format.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "The field {0} must have {1} characters.")]
        public string Plaque { get; set; }

        [Display(Name = "Line")]
        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Line { get; set; }

        [Display(Name = "Color")]
        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Color { get; set; }

        [Display(Name = "Owner")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string UserId { get; set; }

        [Display(Name = "Remarks Descriptions")]
        public string Remarks { get; set; }

        public byte[] Image { get; set; }
    }
}
