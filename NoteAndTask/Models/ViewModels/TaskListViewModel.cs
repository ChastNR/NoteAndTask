using NoteAndTask.Data.Entities;
using System.Collections.Generic;

namespace NoteAndTask.Models.ViewModels
{
    public class TaskListViewModel
    {
        public IEnumerable<TaskEntity> GetAllTasks { get; set; }

        public IEnumerable<TaskList> GetTaskLists { get; set; }
    }
}
