using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }

        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
