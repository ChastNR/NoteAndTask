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
        
        [HttpGet("lists")]
        public IEnumerable<TaskList> Lists() => _repository.GetAll<TaskList>()
            .Where(u => u.UserId == User.Identity.Name)
            .OrderByDescending(c => c.CreationDate).ToList();
        
        [HttpGet("tasks")]
        public IEnumerable<TaskEntity> Tasks(string id, bool archived)
        {
            var tasks = _repository.Get<TaskEntity>();

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

        [Route("addNewList")]
        [HttpPost]
        public async Task<IActionResult> AddNewList(string name)
        {
            if (string.IsNullOrEmpty(name))
                return StatusCode(400, ("Cant add list with empty name ({0}) ", name));
            
            try
            {
                _repository.Create(new TaskList
                {
                    Name = name,
                    UserId = User.Identity.Name
                });
                await _repository.SaveAsync();

                return Ok(("Task list '{0}' added!", name));
            }
            catch (Exception e)
            {
                return Json(("Error: {0}",e));
            }
        }
        
        [HttpPost("addNewTask")]
        public async Task<IActionResult> AddNewTask([FromBody] TaskEntity task)
        {
            if(!ModelState.IsValid) 
                return StatusCode(400, ("Cant add task (id: {0}, name: {1}", task.Id, task.Name));
                
            try
            {
                task.UserId = User.Identity.Name;
                _repository.Create(task);
                await _repository.SaveAsync();

                return Ok("Task added! " + task.Name);
            }
            catch(Exception e)
            {
                return Json(("Error: {0}", e));
            }
        }
        
        [HttpGet("taskDone")]
        public async Task<IActionResult> TaskDone(string id)
        {
            if (string.IsNullOrEmpty(id))
                return StatusCode(409, ("Cant add task (id: {0}) to archive", id));

            try
            {
                var task = _repository.GetById<TaskEntity>(id);
                task.IsDone = true;
                await _repository.SaveAsync();

                return Ok(("Task (id: {0}, name: {1}) moved to archive successfully", id, task.Name));
            }
            catch (Exception e)
            {
                return Json(("Error: {0}", e));
            }
        }

        [HttpGet("deleteTaskList")]
        public async Task<IActionResult> DeleteTaskList(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound("There is no list with id: " + id);

            try
            {
                var list = _repository.GetById<TaskList>(id);
                _repository.Delete(list);

                await _repository.SaveAsync();

                return Ok(("List (id: {0}, name: {1}) removed successfully!", id, list.Name));
            }
            catch (Exception e)
            {
                return Json(("Error: {0}", e));
            }
        }
    }
}