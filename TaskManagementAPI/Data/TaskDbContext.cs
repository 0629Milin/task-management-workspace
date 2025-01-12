using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Models;
using Task = TaskManagementAPI.Models.Task;

namespace TaskManagementAPI.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
