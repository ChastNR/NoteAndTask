using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NoteAndTask.Extensions.EmailSender;
using NoteAndTask.Models.ViewModels;
using Repository.Interface;
using Repository.Models;

namespace NoteAndTask.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IRepository _repository;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IEmailSender _emailSender;

        public AccountController
        (
            IRepository repository,
            IHostingEnvironment appEnvironment,
            IEmailSender emailSender
            )
        {
            _repository = repository;
            _appEnvironment = appEnvironment;
            _emailSender = emailSender;
        }
        
        [HttpGet]
        [Route("getUser")]
        public IActionResult GetUser()
        {
            var user = _repository.GetById<User>(User.Identity.Name);

            return Json(new UserDataViewModel()
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }

        [HttpPost]
        [Route("changeName")]
        public async Task<IActionResult> ChangeName(string oldName, string name)
        {
            if (string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(name))
            {
                return Json(("Please check written data (old name: {0}, new name: {1})", oldName, name));
            }

            try
            {
                var user = _repository.GetById<User>(User.Identity.Name);

                if (user.Name == oldName)
                {
                    return BadRequest("Please, check written name");
                }
                
                user.Name = name;
                await _repository.SaveAsync();
                return Ok("Name changed successfully");
            }
            catch (Exception e)
            {
                return Json(("Error: {0}", e));
            }
        }

        [HttpPost]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword(string oldPassword, string password, string passwordCompare)
        {
            var user = _repository.GetById<User>(User.Identity.Name);

            if (user.PasswordHash != oldPassword || password != passwordCompare)
            {
                return BadRequest("Please, check written passwords");
            }

            try
            {
                user.PasswordHash = password;
                await _repository.SaveAsync();
                return Ok("Password changed successfully");
            }
            catch (Exception e)
            {
                return Json(("Error: {0}", e));
            }
        }
        
        [HttpGet]
        [Route("changeEmailAddress")]
        public async Task<IActionResult> ChangeEmailAddress(string oldEmail, string email)
        {
            var user = _repository.GetById<User>(User.Identity.Name);

            if (user.Email != oldEmail)
            {
                return BadRequest("Please, check written email addresses");
            }

            try
            {
                user.Email = email;
                await _repository.SaveAsync();

                return Ok("Password changed successfully");
            }
            catch (Exception e)
            {
                return Json(("Error: {0}", e));
            }
        }
    }
}