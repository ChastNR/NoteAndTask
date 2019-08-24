using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoteAndTask.Extensions;
using NoteAndTask.Models.ViewModels;
using Repository.Interface;
using Repository.Models;

namespace NoteAndTask.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private AuthOptions AuthOptions { get; }
        public AuthController(IUserRepository userRepository, IOptions<AuthOptions> authOptions)
        {
            _userRepository = userRepository;
            AuthOptions = authOptions.Value;
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] LoginViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return Json($"Please, check written passwords (password: {model.Password}, password confirm: {model.ConfirmPassword})");
            }

            if (!ModelState.IsValid)
            {
                return Unauthorized($"Please check your login and password (login: {model.Login}, password: {model.Password})");
            }
            
            try
            {
              return Ok(GetToken(_userRepository.AuthUser(model.Login).Id));
            }
            catch (Exception e)
            {
                return Json($"Error: {e}");
            }
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] RegisterViewModel model)
        {
            try
            {
                _userRepository.Add(new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = model.Password
                });
                
                return _userRepository.UserExist(model.Email, model.PhoneNumber) != null
                    ? BadRequest("There is another user with the same email or mobile number")
                    : (IActionResult) Ok("Success");
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