using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vehicles.API.Data.Entities;
using Vehicles.Common.Enums;

namespace Vehicles.API.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "You must enter a valid email.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string LastName { get; set; }

        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Document { get; set; }

        [Display(Name = "Address")]
        [MaxLength(100, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        public string Address { get; set; }

        [DefaultValue("57")]
        [Display(Name = "Zipcode")]
        [MaxLength(5, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string CountryCode { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(20, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Photo")]
        public Guid ImageId { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        [Display(Name = "Photo")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Document type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Document type.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public int DocumentTypeId { get; set; }

        public IEnumerable<SelectListItem> DocumentTypes { get; set; }

        [Display(Name = "Photo")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://vehiclesapiidy.azurewebsites.net/images/noimage.png"
            : $"https://Vehiclesidy.blob.core.windows.net/users/{ImageId}";
    }
}
