using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NoteAndTask.Data;
using NoteAndTask.Data.Entities;
using NoteAndTask.Extensions.EmailSender;
using NoteAndTask.Models.ViewModels;

namespace NoteAndTask.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IEmailSender _emailSender;

        public AccountController(ApplicationDbContext db, IHostingEnvironment appEnvironment, IEmailSender emailSender)
        {
            _db = db;
            _appEnvironment = appEnvironment;
            _emailSender = emailSender;
        }
        
        [Route("getUser")]
        [HttpGet]
        public IActionResult GetUser()
        {
            var user = _db.Users.Find(User.Identity.Name);
            
            return Json(new UserDataViewModel()
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }
        
        [Route("changeName")]
        [HttpPost]
        public async Task<IActionResult> ChangeName(string oldName, string name)
        {
            var user = _db.Users.Find(User.Identity.Name);

            if (user.Name != oldName)
                return BadRequest("Please check written name");
            
            user.Name = name;
            await _db.SaveChangesAsync();
            return Ok("Name changed successfully");
        }
        
        [Route("changePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string oldPassword, string password, string passwordCompare)
        {
            var user = _db.Users.Find(User.Identity.Name);

            if (user.PasswordHash != oldPassword || password != passwordCompare)
                return BadRequest("Please check written passwords");
            
            user.PasswordHash = password;
            await _db.SaveChangesAsync();
            return Ok("Password changed successfully");
        }
        
        [Route("changeEmailAddress")]
        [HttpGet]
        public async Task<IActionResult> ChangeEmailAddress(string oldEmail, string email)
        {
            var user = _db.Users.Find(User.Identity.Name);

            if (user.Email != oldEmail)
                return BadRequest("Please check written passwords");
            
            user.Email = email;
            await _db.SaveChangesAsync();
            
            return Ok("Password changed successfully");
        }
    }
}