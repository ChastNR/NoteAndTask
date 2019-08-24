using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Models;

namespace NoteAndTask.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ListController : Controller
    {
        private readonly IListRepository _listRepository;
        public ListController(IListRepository listRepository) => _listRepository = listRepository;
        
        [HttpGet("get")]
        public IEnumerable<TaskList> Get() => _listRepository.Get(Convert.ToInt32(User.Identity.Name));
        
        [HttpPost("add")]
        public IActionResult Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Cant add list with empty name " + name);

            return _listRepository.Add(name, Convert.ToInt32(User.Identity.Name))
                ? (IActionResult) Ok("Task list " + name + " added!")
                : BadRequest("Task list not added");
        }

        [HttpGet("delete")]
        public IActionResult Delete(int? id)
        {
            try
            {
                return _listRepository.Delete(id) ? (IActionResult) Ok("List removed successfully!") : BadRequest("There is no list with id: " + id);
            }
            catch (Exception e)
            {
                return Json("Error: " + e);
            }
        }
    }
}