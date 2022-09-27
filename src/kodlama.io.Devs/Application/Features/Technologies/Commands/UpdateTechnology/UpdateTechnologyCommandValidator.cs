using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommandValidator : AbstractValidator<UpdateTechnologyCommand>
    {
        public UpdateTechnologyCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(1);
            RuleFor(c => c.ProgrammingLanguageId).NotEmpty().GreaterThan(0);
        }
    }
}
