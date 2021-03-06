﻿using Common.Data.Core.Contracts;
using System;
using System.Collections.Generic;
using IssueTracker.Entities;

namespace IssueTracker.Data.Contracts.Repository_Interfaces
{
    public interface IStateWorkflowRepository : IDataRepository<StateWorkflow>
    {
        IEnumerable<StateWorkflow> GetPossibleWorkflows(Guid fromStateId);
    }
}