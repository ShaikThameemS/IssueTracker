namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstateandstateworkflow : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.StateWorkflow",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FromStateId = c.Guid(nullable: false),
                        ToStateId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.State", t => t.FromStateId)
                .ForeignKey("dbo.State", t => t.ToStateId)
                .Index(t => t.FromStateId)
                .Index(t => t.ToStateId);
            
            AddColumn("dbo.Issue", "StateId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Issue", "StateId");
            AddForeignKey("dbo.Issue", "StateId", "dbo.State", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StateWorkflow", "ToStateId", "dbo.State");
            DropForeignKey("dbo.StateWorkflow", "FromStateId", "dbo.State");
            DropForeignKey("dbo.Issue", "StateId", "dbo.State");
            DropIndex("dbo.StateWorkflow", new[] { "ToStateId" });
            DropIndex("dbo.StateWorkflow", new[] { "FromStateId" });
            DropIndex("dbo.Issue", new[] { "StateId" });
            DropColumn("dbo.Issue", "StateId");
            DropTable("dbo.StateWorkflow");
            DropTable("dbo.State");
        }
    }
}
