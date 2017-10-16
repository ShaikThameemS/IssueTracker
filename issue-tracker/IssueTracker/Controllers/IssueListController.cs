using IssueTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IssueTracker.Controllers
{
    public class IssueListController : ApiController
    {
        [HttpGet]
        [Route("api/issuelist")]
        public IEnumerable<Entities.Issue> Get()
        {
            IssueTrackerContext issuelist = new IssueTrackerContext();
            IEnumerable<Entities.Issue> AllIssueList = from allissue in issuelist.Issues.Where(x => x.Active == true) select allissue;
            return AllIssueList;
        }
    }
}
