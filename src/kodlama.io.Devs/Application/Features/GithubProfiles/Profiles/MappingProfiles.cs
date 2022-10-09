using Application.Features.GithubProfiles.Commands.CreateGithubProfile;
using Application.Features.GithubProfiles.Commands.DeleteGithubProfile;
using Application.Features.GithubProfiles.Commands.UpdateGithubProfile;
using Application.Features.GithubProfiles.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<GithubProfile, CreateGithubProfileCommand>().ReverseMap();
            CreateMap<GithubProfile, CreatedGithubProfileDto>().ReverseMap();

            CreateMap<GithubProfile, UpdateGithubProfileCommand>().ReverseMap();
            CreateMap<GithubProfile, UpdatedGithubProfileDto>().ReverseMap();

            CreateMap<GithubProfile, DeleteGithubProfileCommand>().ReverseMap();
            CreateMap<GithubProfile, DeletedGithubProfileDto>().ReverseMap();

            ////getlistquery
            //CreateMap<GithubProfileListModel, IPaginate<GitHubProfile>>().ReverseMap();
            //CreateMap<GithubProfile, GithubProfileListDto>().ReverseMap();
        }
    }
}
