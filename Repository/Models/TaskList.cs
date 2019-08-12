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
        [MaxLength(36)]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<TaskEntity> Tasks { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
