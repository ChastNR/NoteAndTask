using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Interface;

namespace Repository.Models
{
    public class TaskEntity : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        [MaxLength(36)]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsDone { get; set; }

        public DateTime ExpiresOn { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string TaskListId { get; set; }

        public TaskList TaskList { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
