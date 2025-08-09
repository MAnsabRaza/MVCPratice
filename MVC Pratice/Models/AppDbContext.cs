using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_Pratice.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<Student> Student { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Batch> Batch { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Student → Course (No cascade delete)
            modelBuilder.Entity<Student>()
                .HasRequired(s => s.Course)
                .WithMany()
                .HasForeignKey(s => s.courseId)
                .WillCascadeOnDelete(false);

            // Student → Batch (No cascade delete)
            modelBuilder.Entity<Student>()
                .HasRequired(s => s.Batch)
                .WithMany()
                .HasForeignKey(s => s.batchId)
                .WillCascadeOnDelete(false);

            // Student → User (No cascade delete to avoid multiple paths)
            modelBuilder.Entity<Student>()
                .HasRequired(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.userId)
                .WillCascadeOnDelete(false);

            // Course → User (Keep or remove cascade delete as needed)
            modelBuilder.Entity<Course>()
                .HasRequired(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.userId)
                .WillCascadeOnDelete(false);

            // Batch → User (Keep or remove cascade delete as needed)
            modelBuilder.Entity<Batch>()
                .HasRequired(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.userId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
