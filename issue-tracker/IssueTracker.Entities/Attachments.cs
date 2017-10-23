using System;
using Common.Data.Core.Contracts;

namespace IssueTracker.Entities
{
    public class Attachments : IIdentifiableEntity
    {
        public Guid Id { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid IssueId { get; set; }
    }
}