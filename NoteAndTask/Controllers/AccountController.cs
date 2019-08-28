using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteAndTask.Models.ViewModels;
using Repository.Interface;

namespace NoteAndTask.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository) => _userRepository = userRepository;

        [HttpGet("getuser")]
        public IActionResult GetUser()
        {
            var user = _userRepository.GetById(Convert.ToInt32(User.Identity.Name));

            return Json(new UserDataViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }
        //
        //        [Route("changename")]
        //        [HttpPost]
        //        public async Task<IActionResult> ChangeName(string oldName, string name)
        //        {
        //            if (string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(name))
        //            {
        //                return Json("Please check written data (old name: " + oldName + ", new name: " + name + ")");
        //            }
        //
        //            try
        //            {
        //                var user = _repository.GetById<User>(User.Identity.Name);
        //
        //                if (user.Name == oldName)
        //                {
        //                    return BadRequest("Please, check written name");
        //                }
        //
        //                user.Name = name;
        //                await _repository.SaveAsync();
        //                return Ok("Name changed successfully");
        //            }
        //            catch (Exception e)
        //            {
        //                return Json("Error: " + e);
        //            }
        //        }
        //
        //        [Route("changepassword")]
        //        [HttpPost]
        //        public async Task<IActionResult> ChangePassword(string oldPassword, string password, string passwordCompare)
        //        {
        //            var user = _repository.GetById<User>(User.Identity.Name);
        //
        //            if (user.PasswordHash != oldPassword || password != passwordCompare)
        //            {
        //                return BadRequest("Please, check written passwords");
        //            }
        //
        //            try
        //            {
        //                user.PasswordHash = password;
        //                await _repository.SaveAsync();
        //                return Ok("Password changed successfully");
        //            }
        //            catch (Exception e)
        //            {
        //                return Json("Error: " + e);
        //            }
        //        }
        //
        //        [Route("changeemailaddress")]
        //        [HttpGet]
        //        public async Task<IActionResult> ChangeEmailAddress(string oldEmail, string email)
        //        {
        //            var user = _repository.GetById<User>(User.Identity.Name);
        //
        //            if (user.Email != oldEmail)
        //            {
        //                return BadRequest("Please, check written email addresses");
        //            }
        //
        //            try
        //            {
        //                user.Email = email;
        //                await _repository.SaveAsync();
        //
        //                return Ok("Password changed successfully");
        //            }
        //            catch (Exception e)
        //            {
        //                return Json("Error: " + e);
        //            }
        //        }
    }
}