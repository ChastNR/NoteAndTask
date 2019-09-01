using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoteAndTask.Extensions;
using NoteAndTask.Extensions.EmailSender;
using NoteAndTask.Models.ViewModels;
using ProjectModels;
using Repository.Interface;

namespace NoteAndTask.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private AuthOptions AuthOptions { get; }
        private readonly IEmailSender _emailSender;
        
        public AuthController(IUserRepository userRepository, IOptions<AuthOptions> authOptions, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            AuthOptions = authOptions.Value;
            _emailSender = emailSender;
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] LoginViewModel model)
        {
            try
            {
                return !ModelState.IsValid ? (IActionResult)Unauthorized($"Please check your login and password (login: {model.Login}, password: {model.Password})") : Ok(GetToken(_userRepository.AuthUser(model.Login, model.Password).Id));
            }
            catch (Exception e)
            {
                return Json($"Error: {e}");
            }
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] RegisterViewModel model)
        {
            if (_userRepository.UserExist(model.Email, model.PhoneNumber))
            {
                return BadRequest("There is another user with the same email or mobile number");
            }
            
            try
            {
                _userRepository.Add(new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
                });
                
                return Ok("Success");
            }
            catch (Exception e)
            {
                return Json($"Error: {e}");
            }
        }

        private string GetToken(int id)
        {
            var signingCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.SecurityKey)),
                    SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, Convert.ToString(id))
            };

            var token = new JwtSecurityToken(
                AuthOptions.Issuer,
                AuthOptions.Audience,
                expires: DateTime.Now.AddMinutes(AuthOptions.LifeTime),
                signingCredentials: signingCredentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}