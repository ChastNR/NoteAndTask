using Microsoft.EntityFrameworkCore;
using NoteAndTask.Data.Entities;

namespace NoteAndTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
    }
}
