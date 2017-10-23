using System.Data.Entity.ModelConfiguration;
using IssueTracker.Entities;

namespace IssueTracker.Data.Model_Configuration
{
    public class AttachmentsConfiguration : EntityTypeConfiguration<Attachments>
    {
        public AttachmentsConfiguration()
        {
            Property(p => p.Id).IsRequired();
            Property(p => p.FileData).IsRequired();
            Property(p => p.FileName).IsRequired();
            Property(p => p.FileExtension).IsRequired();
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedOn).IsRequired();
            Property(p => p.IssueId).IsRequired();
        }
    }
}
