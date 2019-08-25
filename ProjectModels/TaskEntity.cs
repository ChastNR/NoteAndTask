using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectModels
{
    public class TaskEntity : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsDone { get; set; }

        public DateTime ExpiresOn { get; set; }

        public DateTime CreationDate { get; } = DateTime.Now;

        public int? TaskListId { get; set; }

        public TaskList TaskList { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
