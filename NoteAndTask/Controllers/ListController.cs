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
        private readonly IRepository _repository;
        private readonly IListRepository _listRepository;

        public ListController(IRepository repository, IListRepository listRepository)
        {
            _listRepository = listRepository;
            _repository = repository;
        }

//        [Route("get")]
//        [HttpGet]
//        public IEnumerable<TaskList> Get() => _repository.GetAll<TaskList>()
//            .Where(u => u.UserId == User.Identity.Name)
//            .OrderByDescending(c => c.CreationDate);

        [Route("get")]
        [HttpGet]
        public IEnumerable<TaskList> Get()
        {
           return _listRepository.Get(User.Identity.Name, "CreationDate");
        }
        

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Cant add list with empty name " + name);

            try
            {
                _repository.Create(new TaskList
                {
                    Name = name,
                    UserId = User.Identity.Name
                });
                await _repository.SaveAsync();

                return Ok("Task list " + name + " added!");
            }
            catch (Exception e)
            {
                return Json("Error: " + e);
            }
        }

        [Route("delete")]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("There is no list with id: " + id);

            try
            {
                var list = _repository.GetById<TaskList>(id);
                foreach (var item in _repository.GetAll<TaskEntity>().Where(t => t.TaskListId == list.Id))
                {
                    item.TaskListId = null;
                }
                _repository.Delete(list);

                await _repository.SaveAsync();

                return Ok("List (id: " + id + ", name: " + list.Name + ") removed successfully!");
            }
            catch (Exception e)
            {
                return Json("Error: " + e);
            }
        }
    }
}