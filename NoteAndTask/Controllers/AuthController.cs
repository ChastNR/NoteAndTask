using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IRepository _repository;
        
        private AuthOptions AuthOptions { get; }

        public AuthController
        (
            IRepository repository,
            IOptions<AuthOptions> authOptions
            )
        {
            _repository = repository;
            AuthOptions = authOptions.Value;
        }
        
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return Json(("Please, check written passwords (password: {0}, password confirm: {1})",
                    model.Password, model.ConfirmPassword));
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(401, ("Please check your login and password (login: {0}, password: {1}", model.Login, model.Password));
            }

            try
            {
                var user = await _repository.GetFirstAsync<User>(u =>
                    u.Email == model.Login || u.PhoneNumber == model.Login);

                if (user == null)
                {
                    return Unauthorized();
                }

                return Ok(GetToken(user.Id));
            }
            catch (Exception e)
            {
                return Json(("Error: {0}", e));
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegisterViewModel model)
        {
            if (_repository.GetFirst<User>(u => u.PhoneNumber == model.PhoneNumber || u.Email == model.Email) != null)
            {
                return BadRequest("There is another user with the same email or mobile number");
            }

            try
            {
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = model.Password
                };

                _repository.Create(user);
                await _repository.SaveAsync();

                return Ok(GetToken(_repository.GetFirst<User>(u => u.Email == user.Email || u.PhoneNumber == user.PhoneNumber)?.Id));
            }
            catch (Exception e)
            {
                return Json(("Error: {0}", e));
            }
        }

        private string GetToken(string id)
        {
            var signingCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.SecurityKey)),
                    SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                expires: DateTime.Now.AddMinutes(AuthOptions.LifeTime),
                signingCredentials: signingCredentials,
                claims: new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, id)
                }
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}