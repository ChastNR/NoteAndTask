using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectModels;

namespace ProjectModels
{
    public class User : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public DateTime CreationDate { get; } = DateTime.Now;

        public string UserLogoPath { get; set; }

        public string Token { get; set; }

        public bool EmailNotifications { get; set; }

        public bool SmsNotifications { get; set; }

        public bool Confirmed { get; set; }

        public virtual ICollection<TaskEntity> Tasks { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}


