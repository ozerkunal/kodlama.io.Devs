using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Commands.DeleteGithubProfile
{
    public class DeleteGithubProfileCommandValidator : AbstractValidator<DeleteGithubProfileCommand>
    {
        public DeleteGithubProfileCommandValidator()
        {
            RuleFor(g => g.Id).NotNull();
        }
    }
}
