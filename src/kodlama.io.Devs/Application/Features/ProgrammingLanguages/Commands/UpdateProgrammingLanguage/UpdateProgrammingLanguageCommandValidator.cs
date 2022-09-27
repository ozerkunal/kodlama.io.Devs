using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguageCommandValidator : AbstractValidator<UpdateProgrammingLanguageCommand>
    {
        public UpdateProgrammingLanguageCommandValidator()
        {
            RuleFor(c => c.LanguageName).NotEmpty().WithMessage("Language Name Can Not Be Empty");
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Id Must Be Greater Than 0");
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}
