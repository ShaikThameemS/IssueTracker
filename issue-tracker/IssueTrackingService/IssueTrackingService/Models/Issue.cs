using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTrackingService.Models
{
    public class IssueModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime ProjectCreatedAt { get; set; }
        public Guid StateId { get; set; }
        public Guid ReporterId { get; set; }
        public Guid AssigneeId { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime ResolvedAt { get; set; }
        public string Description { get; set; }
        public Guid CodeNumber { get; set; }
    }
}