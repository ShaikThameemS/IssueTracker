using IssueTracker.Data.Contracts.Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Data.Abstractions;
using IssueTracker.Entities;

namespace IssueTracker.Data.Data_Repositories
{
    public class ProjectRepository : VersionedDataRepository<Project>, IProjectRepository
    {
        private readonly IDbContext _context;
        public ProjectRepository(IDbContext context)
            :base(context)
        {
            _context = context;
        }

        public ICollection<Project> GetProjectsForUser(Guid userId)
        {
            return FindBy(i => i.OwnerId == userId || i.Users.Any(u => u.Id == userId)).ToList();
        }

        public new Project Update(Project project)
        {
            Project existingProject = FindBy(x => x.Id == project.Id).FirstOrDefault();
            if (existingProject != null)
            {
                existingProject.Title = project.Title;
                existingProject.Active = project.Active;
                existingProject.OwnerId = project.OwnerId;
                List<Project_X_Users> Project_X_Users = _context.Set<Project_X_Users>().Where(i => i.ProjectId == project.Id).ToList();
                foreach (Project_X_Users item in Project_X_Users)
                {
                    _context.Set<Project_X_Users>().Remove(item);
                }
                foreach (ApplicationUser item in project.Users)
                {
                    Project_X_Users Project_X_Userss = new Project_X_Users
                    {
                        Id = Guid.NewGuid(),
                        AspNetUsersId = item.Id,
                        ProjectId = project.Id,
                        CreatedAt = DateTime.Now,
                        Active = true
                    };

                    _context.Set<Project_X_Users>().Add(Project_X_Userss);
                }
                _context.SaveChanges();
            }
            return existingProject;
        }
    }
}