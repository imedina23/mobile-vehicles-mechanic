using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models
{
    public class DetailViewModel
    {
        public int Id { get; set; }

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

        public int HistoryId { get; set; }

        [Display(Name = "Process")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Process.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int ProcedureId { get; set; }

        public IEnumerable<SelectListItem> Procedures { get; set; }
    }
}
