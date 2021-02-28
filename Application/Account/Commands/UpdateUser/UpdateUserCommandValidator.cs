using System;
using FluentValidation;

namespace Application.Account.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotEqual(String.Empty);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(128);
            RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}