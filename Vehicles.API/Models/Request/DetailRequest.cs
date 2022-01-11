using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models.Request
{
    public class DetailRequest
    {
        public int Id { get; set; }

        [Display(Name = "History")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int HistoryId { get; set; }

        [Display(Name = "Process")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int ProcedureId { get; set; }

        [Display(Name = "Labor price")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int LaborPrice { get; set; }

        [Display(Name = "Spare Parts Price")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int SparePartsPrice { get; set; }

        [Display(Name = "Remarks Descriptions")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
    }
}
