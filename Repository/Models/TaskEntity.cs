using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Interface;

namespace Repository.Models
{
    public class TaskEntity : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public string Id { get; set; }

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
