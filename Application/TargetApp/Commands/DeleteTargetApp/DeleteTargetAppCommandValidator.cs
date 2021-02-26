using System;
using FluentValidation;

namespace Application.TargetApp.Commands.DeleteTargetApp
{
    public class DeleteTargetAppCommandValidator : AbstractValidator<DeleteTargetAppCommand>
    {
        public DeleteTargetAppCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEqual(Guid.Empty);
        }
    }
}