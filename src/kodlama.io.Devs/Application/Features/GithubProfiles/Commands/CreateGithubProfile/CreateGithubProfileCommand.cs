using Application.Features.GithubProfiles.Dtos;
using Application.Features.GithubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Commands.CreateGithubProfile
{
    public class CreateGithubProfileCommand : IRequest<CreatedGithubProfileDto>
    {
        public int DeveloperId { get; set; }
        public string ProfileUrl { get; set; }

        public class CreateGithubProfileCommandHandler : IRequestHandler<CreateGithubProfileCommand, CreatedGithubProfileDto>
        {
            private readonly IMapper _mapper;
            private readonly IGithubProfileRepository _githubProfileRepository;
            private readonly GithubProfileBusinessRules _githubProfileBusinessRules;
            public CreateGithubProfileCommandHandler(IMapper mapper, IGithubProfileRepository githubProfileRepository, GithubProfileBusinessRules githubProfileBusinessRules)
            {
                _mapper = mapper;
                _githubProfileRepository = githubProfileRepository;
                _githubProfileBusinessRules = githubProfileBusinessRules;
            }
            

            public async Task<CreatedGithubProfileDto> Handle(CreateGithubProfileCommand request, CancellationToken cancellationToken)
            {
                await _githubProfileBusinessRules.GithubProfileCanNotBeDuplicatedWhenInserted(request.DeveloperId);

                GithubProfile mappedGithubProfile = _mapper.Map<GithubProfile>(request);
                GithubProfile createdGithubProfile = await _githubProfileRepository.AddAsync(mappedGithubProfile);
                CreatedGithubProfileDto createdGithubProfileDto = _mapper.Map<CreatedGithubProfileDto>(createdGithubProfile);

                return createdGithubProfileDto;
            }
        }
    }
}
