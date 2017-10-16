namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectUsersLinkingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Project_X_Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AspNetUsersId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users_X_Project",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UsersId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
    }
}
