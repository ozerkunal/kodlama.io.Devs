using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage
{
    public class DeleteProgrammingLanguageCommandValidator : AbstractValidator<DeleteProgrammingLanguageCommand>
    {
        public DeleteProgrammingLanguageCommandValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Id Must Be Greater Than 0");
        }
    }
}
