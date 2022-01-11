using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vehicles.API.Data.Entities
{
    public class Detail
    {
        public int Id { get; set; }

        [Display(Name = "History")]
        [JsonIgnore]
        [Required(ErrorMessage = "The field {0} is required.")]
        public History History { get; set; }

        [Display(Name = "Process")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public Procedure Procedure { get; set; }

        [Display(Name = "Labor price")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int LaborPrice { get; set; }

        [Display(Name = "Spare Parts Price")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int SparePartsPrice { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int TotalPrice => LaborPrice + SparePartsPrice;

        [Display(Name = "Remarks Descriptions")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
    }
}
