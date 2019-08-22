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
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        public TaskController(ITaskRepository taskRepository) => _taskRepository = taskRepository;
        
        [HttpGet("get")]
        public IEnumerable<TaskEntity> Get(int id, bool archived) => _taskRepository.Get(id, archived, Convert.ToInt32(User.Identity.Name));

        [HttpPost("add")]
        public IActionResult Add([FromBody] TaskEntity task)
        {
            try
            {
                task.UserId = Convert.ToInt32(User.Identity.Name);
                _taskRepository.Create(task);

                return Ok("Task added! " + task.Name);
            }
            catch (Exception e)
            {
                return Json("Error: " + e);
            }
        }

//        [HttpGet("done")]
//        public async Task<IActionResult> Done(string id)
//        {
//            if (string.IsNullOrEmpty(id))
//                return BadRequest("Cant add task (id: " + id + ") to archive");
//
//            try
//            {
//                var task = _repository.GetById<TaskEntity>(id);
//                task.IsDone = true;
//                await _repository.SaveAsync();
//
//                return Ok("Task (id: " + id + ", name: " + task.Name + ") moved to archive successfully");
//            }
//            catch (Exception e)
//            {
//                return Json("Error: " + e);
//            }
//        }
    }
}