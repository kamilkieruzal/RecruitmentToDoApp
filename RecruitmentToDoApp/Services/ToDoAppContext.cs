using Microsoft.EntityFrameworkCore;
using RecruitmentToDoApp.Models;

namespace RecruitmentToDoApp.Services
{
    public class ToDoAppContext : DbContext, IToDoAppContext
    {
        public DbSet<ToDo> ToDos { get; set; }

        /// <summary>
        /// FOR TESTS ONLY
        /// </summary>
        public ToDoAppContext()
        {
        }

        public ToDoAppContext(DbContextOptions options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<ToDo>()
                .Property(t => t.Title)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<ToDo>()
                .Property(t => t.CompletePercentage)
                .IsRequired();

            modelBuilder.Entity<ToDo>()
                .Property(t => t.Description)
                .HasMaxLength(1000);

            modelBuilder.Entity<ToDo>()
                .Property(t => t.ExpiryDate);
        }
    }
}
