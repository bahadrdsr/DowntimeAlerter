using FluentValidation;

namespace Application.Account.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().EmailAddress().MaximumLength(128);
            RuleFor(x=>x.DisplayName).NotEmpty().MaximumLength(128);
            RuleFor(x=>x.Password).NotEmpty();
        }
    }
}