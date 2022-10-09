using Application.Features.GithubProfiles.Dtos;
using Application.Features.GithubProfiles.Rules;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Commands.UpdateGithubProfile
{
    public class UpdateGithubProfileCommand : IRequest<UpdatedGithubProfileDto>
    {
        public int Id { get; set; }
        public int DeveloperId { get; set; }
        public string ProfileUrl { get; set; }
        

        public class UpdateGithubProfileCommandHandler : IRequestHandler<UpdateGithubProfileCommand, UpdatedGithubProfileDto>
        {
            private readonly IMapper _mapper;
            private readonly IGithubProfileRepository _githubProfileRepository;
            private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

            public UpdateGithubProfileCommandHandler(IMapper mapper, IGithubProfileRepository githubProfileRepository, GithubProfileBusinessRules githubProfileBusinessRules)
            {
                _mapper = mapper;
                _githubProfileRepository = githubProfileRepository;
                _githubProfileBusinessRules = githubProfileBusinessRules;
            }


            public async Task<UpdatedGithubProfileDto> Handle(UpdateGithubProfileCommand request, CancellationToken cancellationToken)
            {
                GithubProfile? githubProfileUpdated = await _githubProfileRepository.GetAsync(x => x.Id == request.Id);
                _githubProfileBusinessRules.GithubProfileShouldExistWhenRequested(githubProfileUpdated);

                GithubProfile mappedGithubProfile = _mapper.Map<GithubProfile>(request);
                GithubProfile updatedGithubProfile = await _githubProfileRepository.UpdateAsync(mappedGithubProfile);
                UpdatedGithubProfileDto updatedGithubProfileDto = _mapper.Map<UpdatedGithubProfileDto>(updatedGithubProfile);

                return updatedGithubProfileDto;

            }
        }
    }
}
