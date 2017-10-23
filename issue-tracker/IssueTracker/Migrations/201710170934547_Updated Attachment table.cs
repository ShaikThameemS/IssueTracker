namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAttachmenttable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attachments", new[] { "Issue_Id", "Issue_CreatedAt" }, "dbo.Issue");
            DropIndex("dbo.Attachments", new[] { "Issue_Id", "Issue_CreatedAt" });
            DropColumn("dbo.Attachments", "Issue_Id");
            DropColumn("dbo.Attachments", "Issue_CreatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attachments", "Issue_CreatedAt", c => c.DateTime());
            AddColumn("dbo.Attachments", "Issue_Id", c => c.Guid());
            CreateIndex("dbo.Attachments", new[] { "Issue_Id", "Issue_CreatedAt" });
            AddForeignKey("dbo.Attachments", new[] { "Issue_Id", "Issue_CreatedAt" }, "dbo.Issue", new[] { "Id", "CreatedAt" });
        }
    }
}
