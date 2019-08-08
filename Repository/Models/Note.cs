using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Interface;

namespace Repository.Models
{
    public class Note : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate => DateTime.Now;

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
