using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vehicles.API.Data.Entities
{
    public class VehiclePhoto
    {
        public int Id { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "The field {0} is required.")]
        public Vehicle Vehicle { get; set; }

        [Display(Name = "Photo")]
        public Guid ImageId { get; set; }

        [Display(Name = "Photo")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://vehiclesapiidy.azurewebsites.net/images/noimage.png"
            : $"https://Vehiclesidy.blob.core.windows.net/vehiclephotos/{ImageId}";
    }
}
