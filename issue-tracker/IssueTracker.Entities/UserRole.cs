using System;
using Common.Data.Core.Contracts;

namespace IssueTracker.Entities
{
    public class UserRole : IIdentifiableEntity, IVersionableEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public Guid ApplicationUser_Id { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
    }
}
