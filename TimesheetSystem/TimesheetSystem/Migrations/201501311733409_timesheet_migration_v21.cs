namespace TimesheetSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timesheet_migration_v21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "TasksStatus", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "TasksStatus", c => c.Int(nullable: false));
        }
    }
}
