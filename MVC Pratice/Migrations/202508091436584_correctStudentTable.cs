namespace MVC_Pratice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctStudentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "address", c => c.String());
            DropColumn("dbo.Students", "batch");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "batch", c => c.String());
            DropColumn("dbo.Students", "address");
        }
    }
}
