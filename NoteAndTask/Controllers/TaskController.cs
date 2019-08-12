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
        private readonly IRepository _repository;

        public TaskController(IRepository repository) => _repository = repository;

        [Route("get")]
        [HttpGet]
        public IEnumerable<TaskEntity> Get(string id, bool archived) => GetTasks(id, archived);

        #region GetTasks
        private IEnumerable<TaskEntity> GetTasks(string id, bool archived)
        {
            var tasks = _repository.GetAll<TaskEntity>();

            if (id != null)
            {
                return tasks.Where(l => l.TaskListId == id && !l.IsDone && l.UserId == User.Identity.Name);
            }

            if (archived)
            {
                return tasks.Where(t => t.IsDone && t.UserId == User.Identity.Name);
            }

            return tasks.Where(t => t.UserId == User.Identity.Name && !t.IsDone && t.TaskListId == null);
        }
        #endregion GetTasks

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TaskEntity task)
        {
            try
            {
                task.UserId = User.Identity.Name;
                _repository.Create(task);
                await _repository.SaveAsync();

                return Ok("Task added! " + task.Name);
            }
            catch (Exception e)
            {
                return Json("Error: " + e);
            }
        }

        [Route("done")]
        [HttpGet]
        public async Task<IActionResult> Done(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Cant add task (id: " + id + ") to archive");

            try
            {
                var task = _repository.GetById<TaskEntity>(id);
                task.IsDone = true;
                await _repository.SaveAsync();

                return Ok("Task (id: " + id + ", name: " + task.Name + ") moved to archive successfully");
            }
            catch (Exception e)
            {
                return Json("Error: " + e);
            }
        }
    }
}