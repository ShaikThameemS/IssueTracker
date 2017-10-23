using IssueTracker.Entities;
using IssueTrackingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web.Http;

namespace IssueTrackingService.Controllers
{
    public class IssueController : ApiController
    {
        [HttpGet]
        [Route("api/allissue")]
        public IHttpActionResult GetAllIssue()
        {
            Entities issuetrack = new Entities();
            var issuedetail = issuetrack.Issues.Where(x => x.Active == true).Select(x => new
            {x.Active,x.Id,x.CreatedAt,x.Created,x.ProjectId,x.Status,x.Priority,x.ProjectCreatedAt,x.StateId,x.ReporterId,x.Comments,x.AssigneeId}).ToList();                                                   
            return Ok(issuedetail);
        }

        [HttpGet]
        [Route("api/issueTypes")]
        public IHttpActionResult GetAllIssueTypes()
        {
            var issueTypes = new List<IssueTypeList>();
            foreach (var name in Enum.GetNames(typeof(IssueType)))
            {
                issueTypes.Add(new IssueTypeList
                {
                    Id = (int)Enum.Parse(typeof(IssueType), name),
                    Name = name
                });
            }
            return Ok(issueTypes);
        }

        [HttpPost]
        [Route("api/createIssue")]
        public IHttpActionResult CreateIssue(Issue issue)
        {
            if (ModelState.IsValid)
            {
                using (Entities context = new Entities())
                {
                    Guid id = Guid.NewGuid();
                    issue.Id = id;
                    issue.Created = DateTime.Now;
                    issue.CreatedAt = DateTime.Now;
                    var createdAt = context.Projects.Where(x => x.Id == issue.ProjectId).Select(y => y.CreatedAt).FirstOrDefault();
                    issue.ProjectCreatedAt = Convert.ToDateTime(createdAt);
                    context.Issues.Add(issue);
                    context.SaveChanges();
                }
            }

            return Ok(issue.Id);
        }
    }
}
