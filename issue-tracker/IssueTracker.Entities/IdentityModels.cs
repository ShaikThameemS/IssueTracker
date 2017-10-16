using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Common.Data.Core.Contracts;
using System.ComponentModel;

namespace IssueTracker.Entities
{

    public class ApplicationUserRole : IdentityUserRole<Guid>, IIdentifiableEntity
    {
        public Guid Id { get; set; }
    }
    public class ApplicationUserLogin : IdentityUserLogin<Guid> {}
    public class ApplicationUserClaim : IdentityUserClaim<Guid> {}
    public class ApplicationRoleStore : RoleStore<ApplicationRole, Guid, ApplicationUserRole>
    {
        public ApplicationRoleStore(DbContext context)
            : base(context)
        {

        }
    }

    public class ApplicationRoleManager : RoleManager<ApplicationRole, Guid>
    {
        public ApplicationRoleManager(ApplicationRoleStore store) : base(store)
        {

        }
    }

    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>
    {
        public string Description { get; set; }

        public ApplicationRole()
        { }
        public ApplicationRole(string name)
            : this()
        {
            Name = name;
        }

        public ApplicationRole(string name, string description)
            : this(name)
        {
            Description = description;
        }
    }

    public class ApplicatonUserStore :
        UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicatonUserStore(DbContext context)
            : base(context)
        {
        }
    }
}