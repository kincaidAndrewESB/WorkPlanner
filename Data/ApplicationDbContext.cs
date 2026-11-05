using Microsoft.EntityFrameworkCore;
using WorkPlanner.Web.Models;

namespace WorkPlanner.Web.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        { }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Title)
                .HasMaxLength(100);

            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Status)
                .HasMaxLength(20);

            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Priority)
                .HasMaxLength(20);
        }
    }
}
