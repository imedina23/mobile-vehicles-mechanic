using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Vehicle Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a vehicle type.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int VehicleTypeId { get; set; }

        public IEnumerable<SelectListItem> VehicleTypes { get; set; }

        [Display(Name = "Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a brand.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int BrandId { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }

        [Display(Name = "Model")]
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

        public string UserId { get; set; }

        [Display(Name = "Remarks Descriptions")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Display(Name = "Photo")]
        public IFormFile ImageFile { get; set; }

        public ICollection<VehiclePhoto> VehiclePhotos { get; set; }
    }
}
