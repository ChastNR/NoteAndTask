using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectModels;
using Repository.Interface;

namespace NoteAndTask.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        public TaskController(ITaskRepository taskRepository) => _taskRepository = taskRepository;

        [HttpGet("get")]
        public IEnumerable<TaskEntity> Get(int? id, bool archived) => _taskRepository.Get(id, archived, Convert.ToInt32(User.Identity.Name));

        [HttpGet("getall")]
        public IEnumerable<TaskEntity> GetAll() => _taskRepository.GetAll(Convert.ToInt32(User.Identity.Name));

        [HttpPost("add")]
        public IActionResult Add([FromBody] TaskEntity task)
        {
            try
            {
                task.UserId = Convert.ToInt32(User.Identity.Name);
                _taskRepository.Create(task);

                return Ok($"Task added! {task.Name}");
            }
            catch (Exception e)
            {
                return Json($"Error: {e}");
            }
        }

        [HttpGet("done")]
        public IActionResult Done(int? id)
        {
            try
            {
                return _taskRepository.TaskDone(id) ? (IActionResult)Ok("Task moved to archive successfully") : BadRequest($"Cant add task (id: {id}) to archive");
            }
            catch (Exception e)
            {
                return Json($"Error: {e}");
            }
        }
    }
}