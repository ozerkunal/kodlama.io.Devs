using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Commands.CreateGithubProfile
{
    public class CreateGithubProfileCommandValidator : AbstractValidator<CreateGithubProfileCommand>
    {
        public CreateGithubProfileCommandValidator()
        {
            RuleFor(g => g.DeveloperId).NotNull();
            RuleFor(g => g.ProfileUrl).NotEmpty().NotNull();
        }
    }
}
