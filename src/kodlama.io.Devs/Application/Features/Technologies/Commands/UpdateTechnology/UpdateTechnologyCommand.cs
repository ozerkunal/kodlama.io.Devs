using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }

        public class UpdateTechnologyHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public UpdateTechnologyHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                Technology? technologyUpdated = await _technologyRepository.GetAsync(x => x.Id == request.Id);
                _technologyBusinessRules.TechnologyShouldExistWhenRequested(technologyUpdated);
                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicated(request.Id, request.Name);

                Technology mappedtechnology = _mapper.Map<Technology>(request);
                Technology updatedTechnology = await _technologyRepository.UpdateAsync(mappedtechnology);
                UpdatedTechnologyDto updatedTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);

                return updatedTechnologyDto;
            }
        }
    }
}
