using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models.Request
{
    public class HistoryRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public string Remarks { get; set; }
    }
}
