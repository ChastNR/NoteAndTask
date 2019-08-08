using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Interface;

namespace Repository.Models
{
    public class User : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string UserLogoPath { get; set; }

        public string Token { get; set; }

        public bool EmailNotifications { get; set; }

        public bool SmsNotifications { get; set; }

        public bool Confirmed { get; set; }

        public virtual ICollection<TaskEntity> Tasks { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
