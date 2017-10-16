using IssueTracker.Data.Contracts.Repository_Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using IssueTracker.Data.Abstractions;
using IssueTracker.Entities;

namespace IssueTracker.Data.Data_Repositories
{
    public class Project_X_UsersRepository: VersionedDataRepository<Project_X_Users>, IProject_X_UsersRepository
    {
        public Project_X_UsersRepository(IDbContext context) : base (context)
        {
        }
    }
}
