using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Features.Developers.Dtos;
using Application.Features.Developers.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.RegisterDeveloper
{
    public class RegisterDeveloperCommand : IRequest<RegisteredDeveloperDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }

        public class RegisterDeveloperCommandHandler : IRequestHandler<RegisterDeveloperCommand, RegisteredDeveloperDto>
        {
            private readonly DeveloperBusinessRules _developerBusinessRules;
            private readonly IDeveloperRepository _developerRepository;
            private readonly IAuthService _authService;

            public RegisterDeveloperCommandHandler(DeveloperBusinessRules developerBusinessRules, IDeveloperRepository developerRepository, IAuthService authService)
            {
                _developerBusinessRules = developerBusinessRules;
                _developerRepository = developerRepository;
                _authService = authService;
            }

            public async Task<RegisteredDeveloperDto> Handle(RegisterDeveloperCommand request, CancellationToken cancellationToken)
            {
                await _developerBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

                Developer newDeveloper = new()
                {
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    Status = true, 
                };

                Developer createdDeveloper = await _developerRepository.AddAsync(newDeveloper);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdDeveloper);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdDeveloper, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                RegisteredDeveloperDto registeredDeveloperDto = new()
                {
                    RefreshToken = addedRefreshToken,
                    AccessToken = createdAccessToken
                };

                return registeredDeveloperDto;
            }
        }
    }
}
