using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteAndTask.Data;
using NoteAndTask.Data.Entities;

namespace NoteAndTask.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TaskController(ApplicationDbContext db) => _db = db;

//        [Route("lists")]
//        [HttpGet]
        [HttpGet("lists")]
        public IEnumerable<TaskList> Lists() => _db.TaskLists.Where(u => u.UserId == User.Identity.Name)
            .OrderByDescending(c => c.CreationDate).ToList();

//        [Route("tasks")]
//        [HttpGet]
        [HttpGet("tasks")]
        public IEnumerable<TaskEntity> Tasks(string id, bool archived)
        {
            var tasks = _db.Tasks;

            if (id != null)
            {
                return tasks.Where(l => l.TaskListId == id && !l.IsDone && l.UserId == User.Identity.Name).ToList();
            }

            if (archived)
            {
                return tasks.AsNoTracking().Where(t => t.IsDone && t.UserId == User.Identity.Name).ToList();
            }

            return tasks.Where(t => t.UserId == User.Identity.Name && !t.IsDone && t.TaskListId == null).ToList();
        }

        [Route("addNewList")]
        [HttpPost]
        public async Task<IActionResult> AddNewList(string name)
        {
            _db.TaskLists.Add(new TaskList
            {
                Name = name,
                UserId = User.Identity.Name
            });
            await _db.SaveChangesAsync();

            return Ok("Task list added!");
        }

//        [Route("addNewTask")]
//        [HttpPost]
        [HttpPost("addNewTask")]
        public async Task<IActionResult> AddNewTask([FromBody] TaskEntity task)
        {
            task.UserId = User.Identity.Name;
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();

            return Ok("Task added! " + task.Name);
        }
        
//         [HttpGet("taskDone")]
//        public async Task<IActionResult> TaskDone(string id)
//        {
//            if (!string.IsNullOrEmpty(id)) return NotFound("There is no task with id: " + id);
//
//            //var task = _db.Tasks.Find(id);
//            var task = _db.Tasks.FirstOrDefault(t => t.TaskId == id);
//            if (task == null) return NotFound("There is no task with id: " + id);
//            task.IsDone = true;
//            await _db.SaveChangesAsync();
//
//            return Ok("Task (Name: " + task.Name + ") moved to archive successfully!");
//        }
        
        [HttpGet("taskDone")]
        public async Task<IActionResult> TaskDone(string id)
        {
            var task = _db.Tasks.Find(id);
            task.IsDone = true;
            await _db.SaveChangesAsync();

            return Ok("Task (Name: " + task.Name + ") moved to archive successfully!");
        }

        [HttpGet("deleteTaskList")]
        public async Task<IActionResult> DeleteTaskList(string id)
        {
            if (!string.IsNullOrEmpty(id)) return NotFound("There is no list with id: " + id);

            var list = _db.TaskLists.Find(id);
            _db.Remove(list);

            await _db.SaveChangesAsync();

            return Ok("List (Name: " + list.Name + ") removed successfully!");
        }
    }
}