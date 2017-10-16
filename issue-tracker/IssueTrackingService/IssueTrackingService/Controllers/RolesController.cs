using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IssueTrackingService.Controllers
{
  
    public class RolesController : ApiController
    {
        [HttpPost]
        [Route("api/createRole")]        
        public IHttpActionResult CreateRole(AspNetRoles roles)
        {
            if (ModelState.IsValid)
            {
                using (Entities context = new Entities())
                {
                    Guid id = Guid.NewGuid();
                    roles.Id = id;
                    context.AspNetRoles.Add(roles);
                    context.SaveChanges();
                }
            }

            return Ok("Successfully Saved");
        }

        [HttpPost]
        [Route("api/updaterole/{Id}")]
        public IHttpActionResult updateRole(AspNetRoles roles)
        {

            if (ModelState.IsValid)
            {
                using (Entities context = new Entities())
                {
                    AspNetRoles aspNetRole = context.AspNetRoles.Where(role => role.Id == roles.Id).FirstOrDefault();
                    aspNetRole.Name = roles.Name;
                    aspNetRole.Description = roles.Description;
                    context.SaveChanges();
                }
            }
            return Ok("Updated Successfully");
        }

        [HttpGet]
        [Route("api/allroles")]
        public IHttpActionResult GetAllroles()
        {
            Entities Roles = new Entities();
            var rolesdetail = Roles.AspNetRoles.Where(x => x.Name != null).Select(x => new
            { x.Id, x.Name, x.Description }).ToList();
            return Ok(rolesdetail);
        }

        [HttpGet]
        [Route("api/allroles/{Id}")]
        public IHttpActionResult GetAllroles(Guid id)
        {
            Entities Roles = new Entities();
            var rolesdetail = Roles.AspNetRoles.Where(x => x.Id == id).Select(x => new
            { x.Id, x.Name, x.Description }).ToList();
            return Ok(rolesdetail);
        }


        [HttpPost]
        [Route("api/deleterole/{Id}")]
        public IHttpActionResult DeleteRole(Guid id)
        {
            if (ModelState.IsValid)
            {
                using (Entities context = new Entities())
                {
                    AspNetRoles aspNetRole = context.AspNetRoles.Where(role => role.Id == id).FirstOrDefault();
                    context.AspNetRoles.Remove(aspNetRole);
                    context.SaveChanges();
                }
            }
            return Ok("Deleted Successfully");
        }
    }
}
