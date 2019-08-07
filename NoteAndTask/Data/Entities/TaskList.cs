using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteAndTask.Data.Entities
{
    public class TaskList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public string TaskListId { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<TaskEntity> Tasks { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now.Date;

    }
}
