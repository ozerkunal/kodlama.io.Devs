using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Rules
{
    public class DeveloperBusinessRules
    {
        private readonly IDeveloperRepository _developerRepository;

        public DeveloperBusinessRules(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }

        public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            Developer? developer = await _developerRepository.GetAsync(u => u.Email == email);
            if (developer != null) throw new BusinessException("Mail Already Exists");
        }

        public void DeveloperGithubShouldExistWhenRequested(Developer developer)
        {
            if (developer == null) throw new BusinessException("Requested Data does not exists.");
        }
    }
}
