using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;
using Vehicles.API.Helpers;
using Vehicles.API.Models;
using Vehicles.API.Models.Request;
using Vehicles.Common.Enums;

namespace Vehicles.API.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        private readonly IMailHelper _mailHelper;
        private readonly IBlobHelper _blobHelper;

        public AccountController(IUserHelper userHelper, IConfiguration configuration, DataContext context, IMailHelper mailHelper, IBlobHelper blobHelper)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _context = context;
            _mailHelper = mailHelper;
            _blobHelper = blobHelper;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DocumentType documentType = await _context.DocumentTypes.FindAsync(request.DocumentTypeId);
            if (documentType == null)
            {
                return BadRequest("The document type does not exist.");
            }

            User user = await _userHelper.GetUserAsync(request.Email);
            if (user != null)
            {
                return BadRequest("There is already a registered user with that email.");
            }

            Guid imageId = Guid.Empty;
            if (request.Image != null && request.Image.Length > 0)
            {
                imageId = await _blobHelper.UploadBlobAsync(request.Image, "users");
            }

            user = new User
            {
                Address = request.Address,
                CountryCode = request.CountryCode,
                Document = request.Document,
                DocumentType = documentType,
                Email = request.Email,
                FirstName = request.FirstName,
                ImageId = imageId,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserName = request.Email,
                UserType = UserType.User,
            };

            await _userHelper.AddUserAsync(user, request.Password);
            await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString());

            string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            string tokenLink = Url.Action("ConfirmEmail", "Account", new
            {
                userid = user.Id,
                token = myToken
            }, protocol: HttpContext.Request.Scheme);

            _mailHelper.SendMail(user.Email, "Vehicles - Account confirmation", $"<h1>Vehicles - Account confirmation</h1>" +
                $"To enable the user, " +
                $"please click the following link: </br></br><a href = \"{tokenLink}\">Confirm Email</a>");

            return Ok(user);
        }

        [HttpPost]
        [Route("CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(model.Username);
                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.ValidatePasswordAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        return CreateToken(user);
                    }
                }
            }

            return BadRequest();
        }

        private IActionResult CreateToken(User user)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(99),
                signingCredentials: credentials);
            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                user
            };

            return Created(string.Empty, results);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                User user = await _userHelper.GetUserAsync(email);
                if (user != null)
                {
                    IdentityResult result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest(result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    return BadRequest("User not found.");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("SocialLogin")]
        public async Task<IActionResult> SocialLogin(SocialLoginRequest model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(model.Email);
                if (user != null)
                {
                    if (user.LoginType != model.LoginType)
                    {
                        return BadRequest("The user has previously logged in by email or through another social network");
                    }

                    Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.ValidatePasswordAsync(user, model.Id);

                    if (result.Succeeded)
                    {
                        await UpdateUserAsync(user, model);
                        return CreateToken(user);
                    }
                }
                else
                {
                    await CreateUserAsync(model);
                    user = await _userHelper.GetUserAsync(model.Email);
                    return CreateToken(user);
                }
            }

            return BadRequest();
        }

        private async Task UpdateUserAsync(User user, SocialLoginRequest model)
        {
            user.SocialImageURL = model.PhotoURL;
            if (!string.IsNullOrEmpty(model.FirstName))
            {
                user.FirstName = model.FirstName;
            }

            if (!string.IsNullOrEmpty(model.LastName))
            {
                user.LastName = model.LastName;
            }

            await _userHelper.UpdateUserAsync(user);
        }

        private async Task CreateUserAsync(SocialLoginRequest model)
        {
            FirstLastName firstLastName = SeparateFirstAndLastName(model.FullName);
            if (string.IsNullOrEmpty(model.FirstName))
            {
                model.FirstName = firstLastName.FirstName;
            }

            if (string.IsNullOrEmpty(model.LastName))
            {
                model.LastName = firstLastName.LastName;
            }

            User user = new()
            {
                Address = "Pending",
                CountryCode = "57",
                Document = "Pending",
                DocumentType = _context.DocumentTypes.FirstOrDefault(),
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                LoginType = model.LoginType,
                PhoneNumber = "Pending",
                SocialImageURL = model.PhotoURL,
                UserName = model.Email,
                UserType = UserType.User,
            };

            await _userHelper.AddUserAsync(user, model.Id);
            await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString());
            string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);
        }

        private FirstLastName SeparateFirstAndLastName(string fullName)
        {
            int pos = fullName.IndexOf(' ');
            FirstLastName firstLastName = new();
            if (pos == -1)
            {
                firstLastName.FirstName = fullName;
                firstLastName.LastName = fullName;
            }
            else
            {
                firstLastName.FirstName = fullName.Substring(0, pos);
                firstLastName.LastName = fullName.Substring(pos + 1, fullName.Length - pos - 1);
            }

            return firstLastName;
        }

        [HttpPost]
        [Route("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(model.Email);
                if (user == null)
                {
                    return BadRequest("The email entered does not correspond to any user.");
                }

                string myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
                string link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);
                _mailHelper.SendMail(model.Email, "Vehicles - Password reset", $"<h1>Vehicles - Password reset</h1>" +
                    $"To set a new password, click on the following link:</br></br>" +
                    $"<a href = \"{link}\">Cambio de Password</a>");
                return Ok("Instructions for changing your password have been sent to your email.");
            }

            return BadRequest(model);
        }
    }
}
