using System.Data.Entity.ModelConfiguration;
using IssueTracker.Entities;

namespace IssueTracker.Data.Model_Configuration
{
    public class UserRoleConfiguration : EntityTypeConfiguration<ApplicationUserRole>
    {
        public UserRoleConfiguration()
        {
            ToTable("AspNetUserRoles");
        }
    }
}
