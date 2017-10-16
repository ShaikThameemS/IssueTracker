using System.Data.Entity.ModelConfiguration;
using IssueTracker.Entities;

namespace IssueTracker.Data.Model_Configuration
{
    public class Project_X_UsersConfiguration : EntityTypeConfiguration<Project_X_Users>
    {
        public Project_X_UsersConfiguration()
        {
            Property(p => p.ProjectId).IsRequired();
            Property(p => p.AspNetUsersId).IsRequired();
        }
    }
}
