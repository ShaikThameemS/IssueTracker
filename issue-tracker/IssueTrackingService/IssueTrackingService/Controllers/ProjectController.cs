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
    public class ProjectController : ApiController
    {
        [HttpGet]
        [Route("api/projects")]
        public IHttpActionResult GetAllProjects()
        {
            Entities project = new Entities();
            var issuedetail = project.Projects.Where(x => x.Active == true).Select(x => new
            { x.Id, x.Code,x.Title}).ToList();
            return Ok(issuedetail);
        }
    }
}