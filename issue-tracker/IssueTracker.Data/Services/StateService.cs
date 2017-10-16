﻿using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Data.Contracts.Repository_Interfaces;
using IssueTracker.Entities;

namespace IssueTracker.Data.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepo;
        private readonly IStateWorkflowRepository _stateWorkflowRepo;

        public StateService(IStateRepository stateRepository, IStateWorkflowRepository stateWorkflowRepository )
        {
            _stateRepo = stateRepository;
            _stateWorkflowRepo = stateWorkflowRepository;
        }

        public IEnumerable<Guid> GetFinalStateIds()
        {
            var statesWithTransition = _stateWorkflowRepo.Fetch().GroupBy(x => x.FromStateId).Select(g => g.FirstOrDefault()).Select(x => x.FromStateId);
            var allStates = _stateRepo.Fetch().Select(x => x.Id);

            return allStates.Except(statesWithTransition).ToList();
        }

        public ICollection<State> GetInitialStates()
        {
            return _stateRepo.FindBy(s => s.IsInitial).ToList();
        }

        public IEnumerable<State> GetStatesOrderedByIndex()
        {
            return _stateRepo.Fetch().OrderBy(x => x.OrderIndex).ToList();
        }
    }
}