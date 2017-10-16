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
            var issue = new Issue();
        }
    }
}
