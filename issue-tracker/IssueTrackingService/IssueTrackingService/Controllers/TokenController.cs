using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace IssueTrackingService.Controllers
{
    public class TokenController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/token/forall")]
        public IHttpActionResult Get()
        {
            return Ok("hai hari");
        }
        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult AuthorisedUser()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok(identity.Name);
        }
        [Authorize(Roles ="admin")]
        [HttpGet]
        [Route("api/data/authenticateAdmin")]
        public IHttpActionResult AuthorisedAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                .Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            return Ok(identity.Name);
        }

    }
}
