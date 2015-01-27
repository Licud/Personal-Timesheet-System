namespace TimesheetSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timesheet_migration_v2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        ProjectType = c.String(),
                        ProjectDateStarted = c.DateTime(nullable: false),
                        EstimatedProjectDateEnd = c.DateTime(nullable: false),
                        EstimatedDuration = c.Int(nullable: false),
                        TotalDuration = c.Int(nullable: false),
                        ProjectStatus = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TasksId = c.Int(nullable: false, identity: true),
                        TasksName = c.String(),
                        TaskType = c.String(),
                        TaskStartDate = c.DateTime(nullable: false),
                        EstimatedTaskDateEnd = c.DateTime(nullable: false),
                        TaskDuration = c.Int(nullable: false),
                        TasksStatus = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TasksId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.TimeLogs",
                c => new
                    {
                        TimeLogId = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        TimeLogDate = c.DateTime(nullable: false),
                        DurationWorked = c.Int(nullable: false),
                        TasksId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TimeLogId)
                .ForeignKey("dbo.Tasks", t => t.TasksId, cascadeDelete: true)
                .Index(t => t.TasksId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeLogs", "TasksId", "dbo.Tasks");
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropIndex("dbo.TimeLogs", new[] { "TasksId" });
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropTable("dbo.TimeLogs");
            DropTable("dbo.Tasks");
            DropTable("dbo.Projects");
        }
    }
}
