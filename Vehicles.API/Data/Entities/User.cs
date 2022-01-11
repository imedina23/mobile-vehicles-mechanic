using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vehicles.Common.Enums;

namespace Vehicles.API.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string LastName { get; set; }

        [Display(Name = "Document type")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public DocumentType DocumentType { get; set; }

        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Document { get; set; }

        [DefaultValue("57")]
        [Display(Name = "Zipcode")]
        [MaxLength(5, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string CountryCode { get; set; }

        [Display(Name = "Address")]
        [MaxLength(100, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        public string Address { get; set; }

        [Display(Name = "Photo")]
        public Guid ImageId { get; set; }

        [Display(Name = "Photo")]
        public string ImageFullPath => LoginType == LoginType.Email
            ? ImageId == Guid.Empty
                ? $"https://vehiclesapiidy.azurewebsites.net/images/noimage.png"
                : $"https://Vehiclesidy.blob.core.windows.net/users/{ImageId}"
            : string.IsNullOrEmpty(SocialImageURL)
                ? $"https://vehiclesapiidy.azurewebsites.net/images/noimage.png"
                : SocialImageURL;

        [Display(Name = "Photo")]
        public string SocialImageURL { get; set; }

        [Display(Name = "Tipo de login")]
        public LoginType LoginType { get; set; }

        [Display(Name = "Tipo de User")]
        public UserType UserType { get; set; }

        [Display(Name = "User")]
        public string FullName => $"{FirstName} {LastName}";

        public ICollection<Vehicle> Vehicles { get; set; }

        [Display(Name = "# Vehicles")]
        public int VehiclesCount => Vehicles == null ? 0 : Vehicles.Count;
    }
}
