using System;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data.Entities;
using Vehicles.API.Helpers;
using Vehicles.Common.Enums;

namespace Vehicles.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckVehiclesTypeAsync();
            await CheckBrandsAsync();
            await CheckDocumentTypesAsync();
            await CheckProceduresAsync();
            await CheckRolesAsycn();
            await CheckUserAsync("1010", "idy", "Medina", "idy@yopmail.com", "405 311 4620", "St", UserType.Admin);
            await CheckUserAsync("2020", "Joe", "Bob", "joe@yopmail.com", "405 311 4620", "ST", UserType.User);
            await CheckUserAsync("3030", "Lerady", "M", "lerady@yopmail.com", "405 311 4620", "ST", UserType.User);
            await CheckUserAsync("4040", "Luis", "Salazar", "Luis@yopmail.com", "405 311 4620", "ST", UserType.Admin);
        }

        private async Task CheckUserAsync(string document, string firstName, string lastName, string email, string phoneNumber, string address, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Address = address,
                    CountryCode = "73099",
                    Document = document,
                    DocumentType = _context.DocumentTypes.FirstOrDefault(x => x.Description == "Id"),
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    UserName = email,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }
        }

        private async Task CheckRolesAsycn()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckProceduresAsync()
        {
            if (!_context.Procedures.Any())
            {
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Alignment" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Front suspension lubrication" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Rear suspension lubrication" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Front brakes" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Rear brakes" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Front brake fluid" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Rear brake fluid" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Shock Replacement" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Scanning Tool Service" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Motor Oil" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Oil Change" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Air Filter" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Electronic System" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Tire" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Change Front tire" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Change Back Tire" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Repair Moter" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Transmission Rebuilt" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Timing Belt" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Change Battery" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Wash Clean Moter" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Wash" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Spark plug change" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Front bearing change" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Rear bearing change" });
                _context.Procedures.Add(new Procedure { Price = 100, Description = "Accessories" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckDocumentTypesAsync()
        {
            if (!_context.DocumentTypes.Any())
            {
                _context.DocumentTypes.Add(new DocumentType { Description = "DL" });
                _context.DocumentTypes.Add(new DocumentType { Description = "ID" });
                _context.DocumentTypes.Add(new DocumentType { Description = "MI" });
                _context.DocumentTypes.Add(new DocumentType { Description = "Ps" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckBrandsAsync()
        {
            if (!_context.Brands.Any())
            {
                _context.Brands.Add(new Brand { Description = "Ducati" });
                _context.Brands.Add(new Brand { Description = "Harley Davidson" });
                _context.Brands.Add(new Brand { Description = "KTM" });
                _context.Brands.Add(new Brand { Description = "BMW" });
                _context.Brands.Add(new Brand { Description = "Triumph" });
                _context.Brands.Add(new Brand { Description = "Victoria" });
                _context.Brands.Add(new Brand { Description = "Honda" });
                _context.Brands.Add(new Brand { Description = "Suzuki" });
                _context.Brands.Add(new Brand { Description = "Kawasaky" });
                _context.Brands.Add(new Brand { Description = "TVS" });
                _context.Brands.Add(new Brand { Description = "Bajaj" });
                _context.Brands.Add(new Brand { Description = "AKT" });
                _context.Brands.Add(new Brand { Description = "Yamaha" });
                _context.Brands.Add(new Brand { Description = "Chevrolet" });
                _context.Brands.Add(new Brand { Description = "Mazda" });
                _context.Brands.Add(new Brand { Description = "Renault" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckVehiclesTypeAsync()
        {
            if (!_context.VehicleTypes.Any())
            {
                _context.VehicleTypes.Add(new VehicleType { Description = "Car" });
                _context.VehicleTypes.Add(new VehicleType { Description = "Bike" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
