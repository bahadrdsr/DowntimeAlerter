using FluentValidation;

namespace Application.Account.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().EmailAddress();
            RuleFor(x=>x.Password).NotEmpty();
        }
    }
}