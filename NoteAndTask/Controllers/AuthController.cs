using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoteAndTask.Data;
using NoteAndTask.Data.Entities;
using NoteAndTask.Extensions;
using NoteAndTask.Models.ViewModels;

namespace NoteAndTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private AuthOptions AuthOptions { get; }

        public AuthController(ApplicationDbContext db, IOptions<AuthOptions> authOptions)
        {
            _db = db;
            AuthOptions = authOptions.Value;
        }
        
        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] LoginViewModel model)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Login || u.PhoneNumber == model.Login);
            if (user == null) return Unauthorized();
            
            return Ok(GetToken(user.UserId));
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] RegisterViewModel model)
        {
            if (_db.Users.FirstOrDefault(u => u.PhoneNumber == model.PhoneNumber || u.Email == model.Email) != null)
                return BadRequest("There is already a user with the same email and or mobile number");

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = model.Password
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            
            return Ok(GetToken(_db.Users.FirstOrDefault(u => u.Email == user.Email || u.PhoneNumber == user.PhoneNumber)?.UserId));
        }

        [NonAction]
        private string GetToken(string id)
        {
            var signingCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.SecurityKey)), SecurityAlgorithms.HmacSha256Signature);

            //add claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, id)
            };
            
            var token = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                expires: DateTime.Now.AddMinutes(AuthOptions.LifeTime),
                signingCredentials: signingCredentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        
//        [HttpPost("token")]
//        public async Task<IActionResult> GetToken([FromBody] LoginViewModel model)
//        {
//            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Login || u.PhoneNumber == model.Login);
//            if (user == null) return Unauthorized();
//            
//            var signingCredentials =
//                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.SecurityKey)), SecurityAlgorithms.HmacSha256Signature);
//
//            //add claims
//            List<Claim> claims = new List<Claim>
//            {
//                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserId)
//            };
//            
//            var token = new JwtSecurityToken(
//                issuer: AuthOptions.Issuer,
//                audience: AuthOptions.Audience,
//                expires: DateTime.Now.AddMinutes(AuthOptions.LifeTime),
//                signingCredentials: signingCredentials,
//                claims: claims
//            );
//
//            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
//        }
    }
}