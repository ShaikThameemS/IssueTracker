namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedStateandStateWorkflow : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issue", "StateId", "dbo.State");
            DropForeignKey("dbo.StateWorkflow", "FromStateId", "dbo.State");
            DropForeignKey("dbo.StateWorkflow", "ToStateId", "dbo.State");
            DropIndex("dbo.Issue", new[] { "StateId" });
            DropIndex("dbo.StateWorkflow", new[] { "FromStateId" });
            DropIndex("dbo.StateWorkflow", new[] { "ToStateId" });
            DropPrimaryKey("dbo.AspNetUserRoles");
            AddColumn("dbo.AspNetUserRoles", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.Project_X_Users", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Project_X_Users", "Active", c => c.Boolean(nullable: false));
            AddPrimaryKey("dbo.AspNetUserRoles", "Id");
            DropTable("dbo.State");
            DropTable("dbo.StateWorkflow");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StateWorkflow",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FromStateId = c.Guid(nullable: false),
                        ToStateId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        IsInitial = c.Boolean(nullable: false),
                        Colour = c.String(),
                        OrderIndex = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropPrimaryKey("dbo.AspNetUserRoles");
            DropColumn("dbo.Project_X_Users", "Active");
            DropColumn("dbo.Project_X_Users", "CreatedAt");
            DropColumn("dbo.AspNetUserRoles", "Id");
            AddPrimaryKey("dbo.AspNetUserRoles", new[] { "UserId", "RoleId" });
            CreateIndex("dbo.StateWorkflow", "ToStateId");
            CreateIndex("dbo.StateWorkflow", "FromStateId");
            CreateIndex("dbo.Issue", "StateId");
            AddForeignKey("dbo.StateWorkflow", "ToStateId", "dbo.State", "Id");
            AddForeignKey("dbo.StateWorkflow", "FromStateId", "dbo.State", "Id");
            AddForeignKey("dbo.Issue", "StateId", "dbo.State", "Id");
        }
    }
}
