using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Interface;

namespace Repository.Models
{
    public class TaskList : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<TaskEntity> Tasks { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now.Date;

    }
}
