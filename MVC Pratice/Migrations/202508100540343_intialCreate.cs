namespace MVC_Pratice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        current_date = c.DateTime(nullable: false),
                        userId = c.Int(nullable: false),
                        studentId = c.Int(nullable: false),
                        courseId = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.courseId)
                .ForeignKey("dbo.Students", t => t.studentId)
                .ForeignKey("dbo.Users", t => t.userId)
                .Index(t => t.userId)
                .Index(t => t.studentId)
                .Index(t => t.courseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        current_date = c.DateTime(nullable: false),
                        course_name = c.String(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.userId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        phone_number = c.String(),
                        address = c.String(),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        current_date = c.DateTime(nullable: false),
                        student_name = c.String(),
                        age = c.Int(nullable: false),
                        address = c.String(),
                        courseId = c.Int(nullable: false),
                        batchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Batches", t => t.batchId)
                .ForeignKey("dbo.Courses", t => t.courseId)
                .Index(t => t.courseId)
                .Index(t => t.batchId);
            
            CreateTable(
                "dbo.Batches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        current_date = c.DateTime(nullable: false),
                        batch_name = c.String(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.userId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        current_date = c.DateTime(nullable: false),
                        studentId = c.Int(nullable: false),
                        courseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.courseId)
                .ForeignKey("dbo.Students", t => t.studentId)
                .Index(t => t.studentId)
                .Index(t => t.courseId);
            
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        current_date = c.DateTime(nullable: false),
                        studentId = c.Int(nullable: false),
                        courseId = c.Int(nullable: false),
                        userId = c.Int(nullable: false),
                        exam_type = c.String(),
                        marks = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.courseId)
                .ForeignKey("dbo.Students", t => t.studentId)
                .ForeignKey("dbo.Users", t => t.userId)
                .Index(t => t.studentId)
                .Index(t => t.courseId)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Marks", "userId", "dbo.Users");
            DropForeignKey("dbo.Marks", "studentId", "dbo.Students");
            DropForeignKey("dbo.Marks", "courseId", "dbo.Courses");
            DropForeignKey("dbo.Enrollments", "studentId", "dbo.Students");
            DropForeignKey("dbo.Enrollments", "courseId", "dbo.Courses");
            DropForeignKey("dbo.Attendances", "userId", "dbo.Users");
            DropForeignKey("dbo.Attendances", "studentId", "dbo.Students");
            DropForeignKey("dbo.Students", "courseId", "dbo.Courses");
            DropForeignKey("dbo.Students", "batchId", "dbo.Batches");
            DropForeignKey("dbo.Batches", "userId", "dbo.Users");
            DropForeignKey("dbo.Attendances", "courseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "userId", "dbo.Users");
            DropIndex("dbo.Marks", new[] { "userId" });
            DropIndex("dbo.Marks", new[] { "courseId" });
            DropIndex("dbo.Marks", new[] { "studentId" });
            DropIndex("dbo.Enrollments", new[] { "courseId" });
            DropIndex("dbo.Enrollments", new[] { "studentId" });
            DropIndex("dbo.Batches", new[] { "userId" });
            DropIndex("dbo.Students", new[] { "batchId" });
            DropIndex("dbo.Students", new[] { "courseId" });
            DropIndex("dbo.Courses", new[] { "userId" });
            DropIndex("dbo.Attendances", new[] { "courseId" });
            DropIndex("dbo.Attendances", new[] { "studentId" });
            DropIndex("dbo.Attendances", new[] { "userId" });
            DropTable("dbo.Marks");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Batches");
            DropTable("dbo.Students");
            DropTable("dbo.Users");
            DropTable("dbo.Courses");
            DropTable("dbo.Attendances");
        }
    }
}
