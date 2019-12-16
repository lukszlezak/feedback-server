using Gmtl.Feedback.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Gmtl.Feedback.Server.Data
{
    public class FeedbackDbContext : DbContext
    {
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Models.Feedback> Feedbacks { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<User> Users { get; set; }

        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain>()
                .Property(b => b.Name)
                .IsRequired();
            modelBuilder.Entity<Domain>()
                .HasIndex(u => u.Id)
                .IsUnique();

            modelBuilder.Entity<Models.Feedback>()
                .Property(b => b.Message)
                .IsRequired();
            modelBuilder.Entity<Models.Feedback>()
                .Property(f => f.Signature)
                .IsRequired();
            modelBuilder.Entity<Models.Feedback>()
                .HasIndex(u => u.Id)
                .IsUnique();
            modelBuilder.Entity<Models.Feedback>()
               .HasOne(p => p.Domain)
               .WithMany(b => b.Feedbacks);

            modelBuilder.Entity<ApiKey>()
                .Property(k => k.DomainId)
                .IsRequired();
            modelBuilder.Entity<ApiKey>()
                .Property(k => k.KeyValue)
                .IsRequired();
            modelBuilder.Entity<ApiKey>()
               .HasIndex(u => u.KeyValue)
               .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Id)
                .IsUnique();
            modelBuilder.Entity<User>()
                .Property(u => u.Login)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();
        }
    }
}