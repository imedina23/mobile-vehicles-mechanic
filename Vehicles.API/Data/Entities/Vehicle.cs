using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace Vehicles.API.Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Display(Name = "Vehicle Type")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public VehicleType VehicleType { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public Brand Brand { get; set; }

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

        [Display(Name = "Owner")]
        [JsonIgnore]
        [Required(ErrorMessage = "The field {0} is required.")]
        public User User { get; set; }

        [Display(Name = "Remarks Descriptions")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public ICollection<VehiclePhoto> VehiclePhotos { get; set; }

        [Display(Name = "# Photos")]
        public int VehiclePhotosCount => VehiclePhotos == null ? 0 : VehiclePhotos.Count;

        [Display(Name = "Photo")]
        public string ImageFullPath => VehiclePhotos == null || VehiclePhotos.Count == 0
            ? $"https://vehiclesapiidy.azurewebsites.net/images/noimage.png"
            : VehiclePhotos.FirstOrDefault().ImageFullPath;

        public ICollection<History> Histories { get; set; }

        [Display(Name = "# Historys")]
        public int HistoriesCount => Histories == null ? 0 : Histories.Count;
    }
}
