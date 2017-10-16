using System;
using Common.Data.Core.Contracts;

namespace IssueTracker.Entities
{
    public class Project_X_Users : IIdentifiableEntity, IVersionableEntity
    {
        public Guid Id { get; set; }
        public Guid AspNetUsersId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
    }
}
