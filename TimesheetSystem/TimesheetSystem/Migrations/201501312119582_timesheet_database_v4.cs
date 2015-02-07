namespace TimesheetSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timesheet_database_v4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "ProjectName", c => c.String(nullable: false));
            AlterColumn("dbo.Projects", "ProjectType", c => c.String(nullable: false));
            AlterColumn("dbo.Tasks", "TasksName", c => c.String(nullable: false));
            AlterColumn("dbo.Tasks", "TaskType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "TaskType", c => c.String());
            AlterColumn("dbo.Tasks", "TasksName", c => c.String());
            AlterColumn("dbo.Projects", "ProjectType", c => c.String());
            AlterColumn("dbo.Projects", "ProjectName", c => c.String());
        }
    }
}
