using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Interface;

namespace Repository.Models
{
    public class TaskList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<TaskEntity> Tasks { get; set; }

        public DateTime CreationDate { get; } = DateTime.Now;

    }
}
