namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAttachmenttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileData = c.Binary(nullable: false),
                        FileName = c.String(nullable: false),
                        FileExtension = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Guid(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        IssueId = c.Guid(nullable: false),
                        Issue_Id = c.Guid(),
                        Issue_CreatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Issue", t => new { t.Issue_Id, t.Issue_CreatedAt })
                .Index(t => new { t.Issue_Id, t.Issue_CreatedAt });
            
            AddColumn("dbo.Issue", "Status", c => c.String());
            AddColumn("dbo.Issue", "Priority", c => c.String());
            DropColumn("dbo.Issue", "StateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issue", "StateId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Attachments", new[] { "Issue_Id", "Issue_CreatedAt" }, "dbo.Issue");
            DropIndex("dbo.Attachments", new[] { "Issue_Id", "Issue_CreatedAt" });
            DropColumn("dbo.Issue", "Priority");
            DropColumn("dbo.Issue", "Status");
            DropTable("dbo.Attachments");
        }
    }
}
