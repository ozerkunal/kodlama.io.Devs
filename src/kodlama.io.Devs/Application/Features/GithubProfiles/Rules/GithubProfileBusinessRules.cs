using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Rules
{
    public class GithubProfileBusinessRules
    {
        private readonly IGithubProfileRepository _githubProfileRepository;

        public GithubProfileBusinessRules(IGithubProfileRepository githubProfileRepository)
            => _githubProfileRepository = githubProfileRepository;

        public async Task GithubProfileCanNotBeDuplicatedWhenInserted(int developerId)
        {
            GithubProfile result = await _githubProfileRepository.GetAsync(b => b.DeveloperId == developerId);
            if (result != null) throw new BusinessException("Developer is already assigned a GitHub profile");
        }

        public void GithubProfileShouldExistWhenRequested(GithubProfile githubProfile)
        {
            if (githubProfile == null) throw new BusinessException("Requested GitHub profile does not exist");
        }

    }
}
