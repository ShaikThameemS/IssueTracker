using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IssueTrackingService.Controllers
{
    public class UsersController : ApiController
    {
        //// GET: api/Users
        //public IEnumerable<T> Get()
        //{
        //    Entities user = new Entities();
        //    var userDetails = user.AspNetUsers.Select(x => new
        //    { x.Id, x.Email}).ToList();
        //   // return userDetails;
        //}

        // GET: api/Users/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("api/allUsers")]
        public IHttpActionResult GetAllUsers()
        {
            Entities users = new Entities();
            var usersdetails = users.AspNetUsers.Select(x => new { x.Id, x.UserName, x.Email }).ToList();
            return Ok(usersdetails);
        }
    }
}
