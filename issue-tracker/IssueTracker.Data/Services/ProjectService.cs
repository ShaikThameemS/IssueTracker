using IssueTracker.Data.Contracts.Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using IssueTracker.Entities;

namespace IssueTracker.Data.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IIssueRepository _issueRepo;
        private readonly IApplicationUserRepository _userRepo;
        private readonly IProject_X_UsersRepository _projectUsersRepo;

        public ProjectService(IProjectRepository projectRepository, IIssueRepository issueRepository, IApplicationUserRepository applicationUserRepository
            ,IProject_X_UsersRepository project_X_UsersRepository)
        {
            _projectRepo = projectRepository;
            _issueRepo = issueRepository;
            _userRepo = applicationUserRepository;
            _projectUsersRepo = project_X_UsersRepository;
        }

        public Guid? GetProjectId(string code)
        {
            var project = _projectRepo.FindSingleBy(x => x.Code == code);

            return project?.Id;
        }

        public IEnumerable<Project> GetProjects()
        {
            var project = _projectRepo.Fetch()
                .Where(n => n.Active)
                .GroupBy(n => n.Id)
                .Select(g => g.OrderByDescending(x => x.CreatedAt).FirstOrDefault())
                .ToList();
            foreach (var Proj in project)
            {
                var projectUsers = _projectUsersRepo.Fetch().Where(i => i.ProjectId == Proj.Id).ToList();
                foreach (var projUsers in projectUsers)
                {
                    Proj.Users.Add((ApplicationUser)_userRepo.Fetch().Where(i => i.Id == projUsers.AspNetUsersId).FirstOrDefault());
                }
            }
            return project;
        }

        public Project GetProject(string code)
        {
            var project = _projectRepo.Fetch()
                .AsQueryable()
                .Where(p => p.Code == code && p.Active)
                .OrderByDescending(x => x.CreatedAt)
                .Include(p => p.Issues)
                .Include(p => p.Owner)
                .Include(p => p.Users)
                .FirstOrDefault();

            if (project == null)
            {
                return null;
            }

            project.Issues = _issueRepo.Fetch().Where(i => i.ProjectId == project.Id).ToList();
            var projectUsers = _projectUsersRepo.Fetch().Where(i => i.ProjectId == project.Id).ToList();
            foreach (var projUsers in projectUsers)
            {
                project.Users.Add((ApplicationUser)_userRepo.Fetch().Where(i => i.Id == projUsers.AspNetUsersId).FirstOrDefault());
            }
            
            return project;
        }

        public Project GetProject(Guid id)
        {
            var project = _projectRepo.Fetch()
                    .Where(x => x.Id == id)
                    .OrderByDescending(x => x.CreatedAt)
                    .Include(p => p.Issues)
                    .Include(p => p.Owner)
                    .Include(p => p.Users)
                    .FirstOrDefault();
            if (project == null)
            {
                return null;
            }

            project.Issues = _issueRepo.Fetch().Where(i => i.ProjectId == project.Id).ToList();
            var projectUsers = _projectUsersRepo.Fetch().Where(i => i.ProjectId == project.Id).ToList();
            foreach (var projUsers in projectUsers)
            {
                project.Users.Add((ApplicationUser)_userRepo.Fetch().Where(i => i.Id == projUsers.AspNetUsersId).FirstOrDefault());
            }

            return project;
        }

        public IEnumerable<Project> GetProjectsForUser(Guid userId)
        {
            var project = _projectRepo.FindBy(i => i.OwnerId == userId || i.Users.Any(u => u.Id == userId)).ToList();
            foreach (var Proj in project)
            {
                var projectUsers = _projectUsersRepo.Fetch().Where(i => i.ProjectId == Proj.Id).ToList();
                foreach (var projUsers in projectUsers)
                {
                    Proj.Users.Add((ApplicationUser)_userRepo.Fetch().Where(i => i.Id == projUsers.AspNetUsersId).FirstOrDefault());
                }
            }
            return project;
        }

        public void CreateProject(Project project)
        {
            if (ProjectCodeIsNotUnique(project.Code))
            {
                throw new ProjectCodeIsInUseException();
            }

            project.Id = Guid.NewGuid();
            project.Code = project.Code.ToUpper();
            project.CreatedAt = DateTime.Now;
            addProjectOwnerToProjectUsers(project);
            project.Users = _userRepo.FindBy(u => project.SelectedUsers.Contains(u.Id)).ToList();

            _projectRepo.Add(project);
        }

        public bool ProjectCodeIsNotUnique(String code)
        {
            return Enumerable.Any(_projectRepo.GetAll(), p => p.Code.Equals(code.ToUpper()));
        }

        public class ProjectCodeIsInUseException : Exception
        {

        }

        public void EditProject(Project project)
        {
            project.CreatedAt = DateTime.Now;
            addProjectOwnerToProjectUsers(project);
            project.Users = _userRepo.FindBy(u => project.SelectedUsers.Contains(u.Id)).ToList();

            _projectRepo.Update(project);
        }

        public void DeleteProject(string code)
        {
            var projectId = GetProjectId(code);
            if (projectId != null) _projectRepo.Remove(projectId.Value);
        }

        private static void addProjectOwnerToProjectUsers(Project project)
        {
            project.SelectedUsers = project.SelectedUsers?.Union(new[] { project.OwnerId }).ToList() ?? new List<Guid> { project.OwnerId };
        }
    }
}