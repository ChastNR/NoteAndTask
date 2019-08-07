using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteAndTask.Data.Entities
{
    public class TaskEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public string TaskId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }

        public DateTime ExpiresOn { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string TaskListId { get; set; }

        public TaskList TaskList { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
