using Microsoft.EntityFrameworkCore;
using TodoList.Models.Entities;
using Task = TodoList.Models.Entities.Task;

namespace TodoList.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        public DbSet<Task> Tasks { get; set; } = null!;
        public DbSet<Priority> Priorities { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Task -> Priority (one-to-many): chặn xóa Priority khi còn Task đang dùng
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Priority)
                .WithMany(p => p.Tasks)
                .OnDelete(DeleteBehavior.Restrict);

            // Không cho trùng tên Tag
            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.TagName)
                .IsUnique();
        }
    }
}
