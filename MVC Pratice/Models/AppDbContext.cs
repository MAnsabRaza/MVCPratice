using System.Data.Entity;

namespace MVC_Pratice.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<User> User { get; set; }        
        public DbSet<Student> Student { get; set; }   
        public DbSet<Course> Course { get; set; }     
        public DbSet<Batch> Batch { get; set; }      
        public DbSet<Mark> Mark { get; set; }         
        public DbSet<Enrollment> Enrollment { get; set; } 
        public DbSet<Attendance> Attendance { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Student relationships
            modelBuilder.Entity<Student>()
                .HasRequired(s => s.Course)
                .WithMany()
                .HasForeignKey(s => s.courseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasRequired(s => s.Batch)
                .WithMany()
                .HasForeignKey(s => s.batchId)
                .WillCascadeOnDelete(false);

            // Course → User
            modelBuilder.Entity<Course>()
                .HasRequired(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.userId)
                .WillCascadeOnDelete(false);

            // Batch → User
            modelBuilder.Entity<Batch>()
                .HasRequired(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.userId)
                .WillCascadeOnDelete(false);

            // Mark relationships
            modelBuilder.Entity<Mark>()
                .HasRequired(m => m.Student)
                .WithMany()
                .HasForeignKey(m => m.studentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mark>()
                .HasRequired(m => m.Course)
                .WithMany()
                .HasForeignKey(m => m.courseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mark>()
                .HasRequired(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.userId)
                .WillCascadeOnDelete(false);

            // Enrollment relationships
            modelBuilder.Entity<Enrollment>()
                .HasRequired(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.studentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Enrollment>()
                .HasRequired(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.courseId)
                .WillCascadeOnDelete(false);

            // Attendance relationships
            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Student)
                .WithMany()
                .HasForeignKey(a => a.studentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Course)
                .WithMany()
                .HasForeignKey(a => a.courseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.userId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}