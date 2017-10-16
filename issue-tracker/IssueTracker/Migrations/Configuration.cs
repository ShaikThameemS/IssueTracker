using IssueTracker.Entities;

namespace IssueTracker.Migrations
{
    using Data;
    using IssueTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IssueTrackerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        public string PasswordHash { get; private set; }

        protected override void Seed(IssueTrackerContext context)
        {
            //  This method will be called after migrating to the latest version.

            seedUsersAndRoles(context);
        }

        private void seedUsersAndRoles(IssueTrackerContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var roles = new List<IdentityRole>()
            {
                new IdentityRole { Name = UserRoles.Administrators },
                new IdentityRole { Name = UserRoles.Users }
            };

            roles.ForEach(role =>
            {
                if (!context.Roles.Any(r => r.Name == role.Name))
                {
                    roleManager.Create(role);
                }
            });

            var userStore = new ApplicatonUserStore(context);
            var userManager = new ApplicationUserManager(userStore);
            var users = new List<User>
            {
                new User(Guid.NewGuid(), "admin@admin.com", "Password@123", UserRoles.AdministratorsUserRole),
                new User(Guid.NewGuid(), "user@user.com", "Password@123", UserRoles.UsersUserRole)
            };

            users.ForEach(user =>
            {
                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var newUser = new ApplicationUser { Id = user.Id, UserName = user.UserName, Email = user.UserName };
                    userManager.Create(newUser, user.Password);
                    userManager.AddToRole(newUser.Id, user.Role.ToString());
                }
            });
        }

        private class User
        {
            public Guid Id { get; }
            public string UserName { get; }
            public string Password { get; }
            public UserRoles Role { get; }

            public User(Guid id, string userName, string password, UserRoles role)
            {
                Id = id;
                UserName = userName;
                Password = password;
                Role = role;
            }
        }
    }
}