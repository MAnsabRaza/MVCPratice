namespace MVC_Pratice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        current_date = c.DateTime(nullable: false),
                        student_name = c.String(),
                        age = c.Int(nullable: false),
                        batch = c.String(),
                        userId = c.Int(nullable: false),
                        courseId = c.Int(nullable: false),
                        batchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Batches", t => t.batchId)
                .ForeignKey("dbo.Courses", t => t.courseId)
                .ForeignKey("dbo.Users", t => t.userId)
                .Index(t => t.userId)
                .Index(t => t.courseId)
                .Index(t => t.batchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "userId", "dbo.Users");
            DropForeignKey("dbo.Students", "courseId", "dbo.Courses");
            DropForeignKey("dbo.Students", "batchId", "dbo.Batches");
            DropForeignKey("dbo.Courses", "userId", "dbo.Users");
            DropForeignKey("dbo.Batches", "userId", "dbo.Users");
            DropIndex("dbo.Students", new[] { "batchId" });
            DropIndex("dbo.Students", new[] { "courseId" });
            DropIndex("dbo.Students", new[] { "userId" });
            DropIndex("dbo.Courses", new[] { "userId" });
            DropIndex("dbo.Batches", new[] { "userId" });
            DropTable("dbo.Students");
            DropTable("dbo.Courses");
            DropTable("dbo.Users");
            DropTable("dbo.Batches");
        }
    }
}
