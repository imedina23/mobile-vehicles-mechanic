using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace Vehicles.API.Data.Entities
{
    public class History
    {
        public int Id { get; set; }

        [Display(Name = "Vehicle")]
        [JsonIgnore]
        [Required(ErrorMessage = "The field {0} is required.")]
        public Vehicle Vehicle { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime Date { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime DateLocal => Date.ToLocalTime();

        [Display(Name = "Miles")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Mileage { get; set; }

        [Display(Name = "Remarks Descriptions")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [JsonIgnore]
        [Display(Name = "Mechanic")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public User User { get; set; }

        public ICollection<Detail> Details { get; set; }

        [Display(Name = "# Details")]
        public int DetailsCount => Details == null ? 0 : Details.Count;

        [Display(Name = "Total Labor")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int TotalLabor => Details == null ? 0 : Details.Sum(x => x.LaborPrice);

        [Display(Name = "Total Spare Parts")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int TotalSpareParts => Details == null ? 0 : Details.Sum(x => x.SparePartsPrice);

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int Total => Details == null ? 0 : Details.Sum(x => x.TotalPrice);
    }
}
