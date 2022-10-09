using Application.Features.GithubProfiles.Dtos;
using Application.Features.GithubProfiles.Rules;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Commands.DeleteGithubProfile
{
    public class DeleteGithubProfileCommand : IRequest<DeletedGithubProfileDto>
    {
        public int Id { get; set; }

        public class DeleteGithubProfileCommandQuery : IRequestHandler<DeleteGithubProfileCommand, DeletedGithubProfileDto>
        {
            private readonly IMapper _mapper;
            private readonly IGithubProfileRepository _githubProfileRepository;
            private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

            public DeleteGithubProfileCommandQuery(IMapper mapper, IGithubProfileRepository githubProfileRepository, GithubProfileBusinessRules githubProfileBusinessRules)
            {
                _mapper = mapper;
                _githubProfileRepository = githubProfileRepository;
                _githubProfileBusinessRules = githubProfileBusinessRules;
            }


            public async Task<DeletedGithubProfileDto> Handle(DeleteGithubProfileCommand request, CancellationToken cancellationToken)
            {
                GithubProfile? githubProfileDeleted = await _githubProfileRepository.GetAsync(g => g.Id == request.Id);
                _githubProfileBusinessRules.GithubProfileShouldExistWhenRequested(githubProfileDeleted);

                GithubProfile deletedGitHubProfile = await _githubProfileRepository.DeleteAsync(githubProfileDeleted);
                DeletedGithubProfileDto deletedGithubProfileDto = _mapper.Map<DeletedGithubProfileDto>(deletedGitHubProfile);

                return deletedGithubProfileDto;
            }
        }
    }
}
