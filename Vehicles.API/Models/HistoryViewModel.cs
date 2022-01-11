using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models
{
    public class HistoryViewModel
    {
        public int VehicleId { get; set; }

        [Display(Name = "Miles")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int Mileage { get; set; }

        [Display(Name = "Remarks Descriptions")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Remarks { get; set; }
    }
}
