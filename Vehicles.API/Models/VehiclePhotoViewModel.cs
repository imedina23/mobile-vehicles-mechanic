using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models
{
    public class VehiclePhotoViewModel
    {
        public int VehicleId { get; set; }

        [Display(Name = "Photo")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public IFormFile ImageFile { get; set; }
    }
}
