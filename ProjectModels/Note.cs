using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectModels
{
    public class Note : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate => DateTime.Now;

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
