﻿using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;
using AutoMapper;
using IssueTracker.Entities;
using IssueTracker.ViewModels;
using PagedList;
using IssueTracker.Data.Contracts.Repository_Interfaces;
using System.Text.RegularExpressions;
using IssueTracker.Models;
using IssueTracker.Data.Services;
using IssueTracker.Data.Facade;

namespace IssueTracker.Controllers
{
    [AuthorizeOrErrorPage]
    public class IssuesController : Controller
    {
        private readonly IIssueService _issueService;
        private readonly IStateService _stateService;
        private readonly IStateWorkflowRepository _stateWorkflowRepo;
        private readonly IApplicationUserRepository _applicationUserRepo;
        private readonly IStateRepository _stateRepo;
        private readonly IProjectService _projectService;

        private const int ProjectsPerPage = 20;

        public IssuesController(IFacade facade, IStateWorkflowRepository stateWorkflowRepository, IApplicationUserRepository applicationUserRepository,
            IStateRepository stateRepository, IIssueService issueService, IStateService stateService, IProjectService projectService)
        {
            _facade = facade;
            _projectService = projectService;
            _issueService = issueService;
            _stateService = stateService;
            _stateWorkflowRepo = stateWorkflowRepository;
            _applicationUserRepo = applicationUserRepository;
            _stateRepo = stateRepository;
        }

        // GET: Issues
        public ActionResult Index(int? page, string sort, string searchName, string searchTitle,
            Guid? searchAssignee, Guid? searchReporter, Guid? searchProject, Guid? searchState, IssueType? searchType)
        {
            // viewbag items are used in the header to sort the records
            ViewBag.CreatedSort = String.IsNullOrEmpty(sort) ? "created_desc" : String.Empty;
            ViewBag.SummarySort = sort == "summary" ? "summary_desc" : "summary";
            ViewBag.ReporterSort = sort == "reporter" ? "reporter_desc" : "reporter";
            ViewBag.ProjectSort = sort == "project" ? "project_desc" : "project";
            ViewBag.AssigneeSort = sort == "assignee" ? "assignee_desc" : "assignee";
            ViewBag.StatusSort = sort == "status" ? "status_desc" : "status";
            ViewBag.IssueTypeSort = sort == "issuetype" ? "issuetype_desc" : "issuetype";

            ViewBag.SearchProject = new SelectList(_projectService.GetProjects(), "Id", "Title");
            ViewBag.SearchAssignee = ViewBag.SearchReporter = new SelectList(_applicationUserRepo.GetAll(), "Id", "Email");
            ViewBag.SearchState = new SelectList(_stateService.GetStatesOrderedByIndex(), "Id", "Title");
            
            var issuesTemp = _issueService.GetAllIssues().ToList();

            // search
            issuesTemp = searchIssues(issuesTemp, searchName, searchTitle, searchAssignee, searchReporter, searchProject, searchState, searchType);

            var issues = Mapper.Map<IEnumerable<IssueIndexViewModel>>(issuesTemp);
            issues = getSortedIssues(issues, sort);

            var pageNumber = page ?? 1;

            return View(issues.ToPagedList(pageNumber, ProjectsPerPage));
        }
        [HttpGet]
        public ActionResult GetIssue(int? page, string sort, string searchName, string searchTitle,
            Guid? searchAssignee, Guid? searchReporter, Guid? searchProject, Guid? searchState, IssueType? searchType)
        {
            var issuesTemp = _issueService.GetAllIssues().ToList();

            // search
            issuesTemp = searchIssues(issuesTemp, searchName, searchTitle, searchAssignee, searchReporter, searchProject, searchState, searchType);

            var issues = Mapper.Map<IEnumerable<IssueIndexViewModel>>(issuesTemp);
            
            return Json(issues);
        }

        private static List<Issue> searchIssues(List<Issue> issues, string searchName, string searchTitle,
            Guid? searchAssignee, Guid? searchReporter, Guid? searchProject, Guid? searchState, IssueType? searchType)
        {
            if (!string.IsNullOrEmpty(searchName))
            {
                issues = issues.Where(s => s.Name.ToLower().Contains(searchName.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(searchTitle))
            {
                issues = issues.Where(s => (s.Project.Code + s.CodeNumber + ": " + s.Name).ToLower().Contains(searchTitle.ToLower())).ToList();
            }

            if (searchAssignee != null)
            {
                issues = issues.Where(s => s.AssigneeId == searchAssignee).ToList();
            }

            if (searchReporter != null)
            {
                issues = issues.Where(s => s.ReporterId == searchReporter).ToList();
            }

            if (searchProject != null)
            {
                issues = issues.Where(s => s.ProjectId == searchProject).ToList();
            }

            if (searchState != null)
            {
                issues = issues.Where(s => s.StateId == searchState).ToList();
            }

            if (searchType != null)
            {
                issues = issues.Where(s => s.Type == searchType).ToList();
            }

            return issues;
        }

        private readonly UsersByEmailComparer _usersComparer = new UsersByEmailComparer();
        private readonly ProjectsByTitleComparer _projectsComparer = new ProjectsByTitleComparer();
        private readonly StatesByTitleComparer _statesComparer = new StatesByTitleComparer();
        private readonly IFacade _facade;

        private IEnumerable<IssueIndexViewModel> getSortedIssues(IEnumerable<IssueIndexViewModel> issues, string sortKey)
        {
            switch (sortKey)
            {
                case "summary":
                    return issues.OrderBy(ii => ii.Name);
                case "summary_desc":
                    return issues.OrderByDescending(ii => ii.Name);
                case "reporter_desc":
                    return issues.OrderByDescending(ii => ii.Reporter, _usersComparer);
                case "reporter":
                    return issues.OrderBy(ii => ii.Reporter, _usersComparer);
                case "status_desc":
                    return issues.OrderByDescending(ii => ii.State, _statesComparer);
                case "status":
                    return issues.OrderBy(ii => ii.State, _statesComparer);
                case "assignee_desc":
                    return issues.OrderByDescending(ii => ii.Assignee, _usersComparer);
                case "assignee":
                    return issues.OrderBy(ii => ii.Assignee, _usersComparer);
                case "project_desc":
                    return issues.OrderByDescending(ii => ii.Project, _projectsComparer);
                case "project":
                    return issues.OrderBy(ii => ii.Project, _projectsComparer);
                case "created_desc":
                    return issues.OrderByDescending(ii => ii.Created);
                case "issuetype":
                    return issues.OrderBy(ii => ii.Type);
                case "issuetype_desc":
                    return issues.OrderByDescending(ii => ii.Type);
                default:
                    return issues.OrderBy(ii => ii.Created);
            }
        }

        private class UsersByEmailComparer : IComparer<ApplicationUser>
        {
            public int Compare(ApplicationUser x, ApplicationUser y)
            {
                if (x == null && y == null)
                {
                    return 0;
                } 
                else if (x == null && y != null)
                {
                    return 1;
                }
                else if (x != null && y == null)
                {
                    return -1;
                }
                return x.Email.CompareTo(y.Email);
            }
        }

        private class ProjectsByTitleComparer : IComparer<ProjectViewModel>
        {
            public int Compare(ProjectViewModel x, ProjectViewModel y)
            {
                return x.Title.CompareTo(y.Title);
            }
        }

        private class StatesByTitleComparer : IComparer<StateViewModel>
        {
            public int Compare(StateViewModel x, StateViewModel y)
            {
                return x.Title.CompareTo(y.Title);
            }
        }

        // GET: Issues/Details/5
        public ActionResult Details(string id, IssueSubDetail? sub)
        {
            var code = IssueCode.Parse(id);
            if (code == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var issue = _issueService.GetByProjectCodeAndIssueNumber(code.ProjectCode, code.IssueNumber);

            var viewModel = new IssueDetailViewModel
            {
                Issue = Mapper.Map<IssueIndexViewModel>(issue)
            };

            if (viewModel.Issue == null)
            {
                return HttpNotFound();
            }

            // possible workflows
            var workflows = _stateWorkflowRepo.GetPossibleWorkflows(viewModel.Issue.State.Id);
            foreach (var stateWorkflow in workflows)
            {
                stateWorkflow.ToState = _stateRepo.Get(stateWorkflow.ToStateId);
            }
            viewModel.StateWorkflows = Mapper.Map<IEnumerable<StateWorkflowViewModel>>(workflows);

            // comments from all versions of the issue
            var comments = _issueService.GetCommentsForIssue(issue.Id);

            viewModel.Comments = Mapper.Map<IEnumerable<CommentViewModel>>(comments);
            foreach (var comment in viewModel.Comments)
            {
                comment.User = _applicationUserRepo.Get(comment.AuthorId);
            }

            viewModel.Changes = getHistory(viewModel.Issue.Id, comments);

            ViewBag.Sub = sub.ToString() == "" ? "Comments" : sub.ToString();
            ViewBag.LoggedUser = getLoggedUser();
            ViewBag.IsUserAdmin = User.IsInRole(UserRoles.Administrators);
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;

            return View(viewModel);
        }

        /// <summary>
        /// Creates history of changes and added comments within given issue.
        /// </summary>
        /// <param name="id">Id of the issue</param>
        /// <param name="comments">Comments from all versions of the issue</param>
        /// <returns>List of all changes and comments</returns>
        private List<IssueChange> getHistory(Guid id, IEnumerable<Comment> comments)
        {
            var allVersions = _issueService.GetAllVersions(id);
            var changes = new List<IssueChange>();

            // find all changes within the issue
            Issue previousVersion = null;
            foreach (var version in allVersions)
            {
                if (previousVersion != null)
                {
                    changes.AddRange(findChange(previousVersion, version));
                }
                previousVersion = version;
            }

            // add comments into history
            changes.AddRange(comments.Select(comment => new IssueChange(comment.CreatedAt, IssueChangeType.Comment, "", comment.Text)));

            return changes.OrderBy(x => x.Changed).ToList();
        }

        /// <summary>
        /// Finds changes within issue between given version and its previous variant (k and k-1).
        /// </summary>
        /// <param name="previous">Previous version of issue</param>
        /// <param name="current">Any version of the issue</param>
        /// <returns>List of found changes</returns>
        private IEnumerable<IssueChange> findChange(Issue previous, Issue current)
        {
            var foundChanges = new List<IssueChange>();

            if (previous.Name != current.Name)
            {
                foundChanges.Add(new IssueChange(current.CreatedAt, IssueChangeType.Title, previous.Name, current.Name));
            }
            else if (previous.StateId != current.StateId)
            {
                foundChanges.Add(new IssueChange(current.CreatedAt, IssueChangeType.State, previous.State.Title, current.State.Title));
            }
            else if (previous.Type != current.Type)
            {
                foundChanges.Add(new IssueChange(current.CreatedAt, IssueChangeType.Type, previous.Type.ToString(), current.Type.ToString()));
            }
            else if (previous.AssigneeId != null && current.AssigneeId != null && previous.AssigneeId != current.AssigneeId)
            {
                foundChanges.Add(new IssueChange(current.CreatedAt, IssueChangeType.Assignee, previous.Assignee.Email, current.Assignee.Email));
            }
            else if (previous.Description != current.Description)
            {
                foundChanges.Add(new IssueChange(current.CreatedAt, IssueChangeType.Description, previous.Description, current.Description));
            }

            return foundChanges;
        }

        // GET: Issues/Create
        public ActionResult Create()
        {
            ViewBag.ErrorSQL = TempData["ErrorSQL"] as string;
            ViewBag.ErrorInvalidProject = TempData["ErrorInvalidProject"] as string;
            ViewBag.AssigneeId = new SelectList(Enumerable.Empty<UserEmailViewModel>(), "Id", "Email");
            ViewBag.ProjectId = new SelectList(_projectService.GetProjectsForUser(getLoggedUser().Id), "Id", "Title");
            ViewBag.ReporterId = getLoggedUser().Id;

            return View();
        }

        // POST: Issues/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(IssueCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var users = loadProjectUsersAsUserEmailViewModel(viewModel.ProjectId);
                ViewBag.AssigneeId = new SelectList(users, "Id", "Email", viewModel.AssigneeId);
                ViewBag.ProjectId = new SelectList(_projectService.GetProjectsForUser(getLoggedUser().Id), "Id", "Title", viewModel.ProjectId);
                return View(viewModel);
            }
            var initialState = getInitialState();
            if (initialState == null)
            {
                TempData["ErrorSQL"] = Locale.IssueStrings.ErrorMessageNoIniState;
                return RedirectToAction("Create");
            }

            var projectTemp = _projectService.GetProject(viewModel.ProjectId);
            if (!projectTemp.Users.Any(u => u.Id == getLoggedUser().Id))
            {
                TempData["ErrorInvalidProject"] = Locale.IssueStrings.ErrorMessageInvalidProjectCreate;
                return RedirectToAction("Create");
            }

            var issue = Mapper.Map<Issue>(viewModel);
            issue.StateId = initialState.Id;
            issue.ReporterId = getLoggedUser().Id;
            issue.Created = DateTime.Now;
            issue.ProjectCreatedAt = projectTemp.CreatedAt;
            issue.Id = Guid.NewGuid();
            issue.Status = "Open";
            issue.CodeNumber = _issueService.GetNewCodeNumber();

            _issueService.Add(issue);

            return RedirectToAction("Details", new { id = issue.Code });

        }

        [HttpPost]
        public ActionResult LoadProjectUsers(string id)
        {
            var project = _projectService.GetProject(Guid.Parse(id));
            var users = projectUsersToUserEmailViewModel(project);
            return Json(users);
        }

        private IEnumerable<UserEmailViewModel> projectUsersToUserEmailViewModel(Project project)
        {
            if (project == null)
            {
                return Enumerable.Empty<UserEmailViewModel>();
            }
            return from user in project.Users
                   select new UserEmailViewModel { Id = user.Id.ToString(), Email = user.Email };
        }

        private IEnumerable<UserEmailViewModel> loadProjectUsersAsUserEmailViewModel(Guid projectId)
        {
            var project = _projectService.GetProject(projectId);
            var users = projectUsersToUserEmailViewModel(project);
            
            return users;
        }

        private State getInitialState()
        {
            try
            {
                return _stateRepo.GetInitialState();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private ApplicationUser getLoggedUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _applicationUserRepo.GetAll().First(dbUser => dbUser.Email == User.Identity.Name);
                return user;
            }
            return null;
        }

        // GET: Issues/Edit/5
        public ActionResult Edit(string id)
        {
            var code = IssueCode.Parse(id);
            if (code == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.ErrorInvalidProject = TempData["ErrorInvalidProject"] as string;

            var issue = _issueService.GetByProjectCodeAndIssueNumber(code.ProjectCode, code.IssueNumber);

            if (issue == null)
                return HttpNotFound();

            var viewModel = Mapper.Map<IssueEditViewModel>(issue);

            ViewBag.AssigneeId = new SelectList(loadProjectUsersAsUserEmailViewModel(issue.ProjectId), "Id", "Email", issue.AssigneeId);
            ViewBag.ProjectId = new SelectList(_projectService.GetProjectsForUser(getLoggedUser().Id), "Id", "Title", issue.ProjectId);
            ViewBag.StateId = new SelectList(_stateService.GetStatesOrderedByIndex(), "Id", "Title", issue.StateId);

            return View(viewModel);
        }

        // POST: Issues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code,Name,Type,Description,AssigneeId,ProjectId")] IssueEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AssigneeId = new SelectList(loadProjectUsersAsUserEmailViewModel(viewModel.ProjectId), "Id", "Email", viewModel.AssigneeId);
                ViewBag.ProjectId = new SelectList(_projectService.GetProjectsForUser(getLoggedUser().Id), "Id", "Title", viewModel.ProjectId);
                return View(viewModel);
            }
            var code = IssueCode.Parse(viewModel.Code);
            if (code == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var projectTemp = _projectService.GetProject(viewModel.ProjectId);
            if (!projectTemp.Users.Any(u => u.Id == getLoggedUser().Id))
            {
                TempData["ErrorInvalidProject"] = Locale.IssueStrings.ErrorMessageInvalidProjectEdit;
                return RedirectToAction("Edit", "Issues", new { id = viewModel.Code });
            }

            // create a new entity
            var entityNew = _issueService.GetNewEntityForEditing(code.ProjectCode, code.IssueNumber);
            entityNew.Reporter = null;
            entityNew.Assignee = null;
            entityNew.State = null;
            entityNew.Comments = null;
            entityNew.Project = null;

            // in case the project was changed
            if (viewModel.ProjectId != entityNew.ProjectId)
            {
                entityNew.ProjectCreatedAt = projectTemp.CreatedAt;
            }
            // map viewModel to the entity
            entityNew = Mapper.Map(viewModel, entityNew);

            if(viewModel.AssigneeId == null)
            {
                entityNew.AssigneeId = null;
            }

            // change CreatedAt
            entityNew.CreatedAt = DateTime.Now;
            // save the entity
            _issueService.Add(entityNew);

            return RedirectToAction("Details", new { id = entityNew.Code });
        }

        public ActionResult ChangeStatus(Guid issueId, Guid to)
        {
            // create a new entity
            var entityNew = _issueService.GetNewEntityForEditing(issueId);
            if (entityNew != null)
            {
                // change status
                entityNew.StateId = to;
                // change CreatedAt
                entityNew.CreatedAt = DateTime.Now;
                // mark as resolved if in final state
                if (entityNew.ResolvedAt == null && _facade.IsIssueInFinalState(entityNew))
                {
                    entityNew.ResolvedAt = DateTime.Now;
                }
                // save the entity
                entityNew.Reporter = null;
                entityNew.Assignee = null;
                entityNew.State = null;
                entityNew.Comments = null;
                entityNew.Project = null;
                _issueService.Add(entityNew);
            }

            if (HttpContext.Request.UrlReferrer != null)
            {
                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }

            return RedirectToAction("Index");
        }

        private class IssueCode
        {

            public string ProjectCode;
            public int IssueNumber;

            public static IssueCode Parse(string code)
            {
                if (code == null || !matchIssueCodePattern(code))
                    return null;

                var splittedCode = code.Split('-');
                var projectCode = splittedCode[0];
                var issueNumber = int.Parse(splittedCode[1]);

                if (projectCode == null || issueNumber == 0)
                    return null;
                return new IssueCode { ProjectCode = projectCode, IssueNumber = issueNumber };
            }

            private static bool matchIssueCodePattern(string s)
            {
                var rgx = new Regex(@"^[A-Z]+[-][0-9]+$"); // E.g.: CODE-19
                return rgx.IsMatch(s);
            }

            private IssueCode()
            {
            }
        }

        public class IssueChange
        {
            public DateTime Changed { get; set; }
            public IssueChangeType Type { get; set; }
            [AllowHtml]
            public string From { get; set; }
            [AllowHtml]
            public string To { get; set; }

            public IssueChange(DateTime changed, IssueChangeType type, string from, string to)
            {
                Changed = changed;
                Type = type;
                From = from;
                To = to;
            }
        }
    }
}
