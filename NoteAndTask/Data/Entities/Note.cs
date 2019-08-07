using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteAndTask.Data.Entities
{
    public class Note
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public string NoteId { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate => DateTime.Now;

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
