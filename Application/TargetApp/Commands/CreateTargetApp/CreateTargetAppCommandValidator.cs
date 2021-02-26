using FluentValidation;

namespace Application.TargetApp.Commands.CreateTargetApp
{
    public class CreateTargetAppCommandValidator : AbstractValidator<CreateTargetAppCommand>
    {
        public CreateTargetAppCommandValidator()
        {
            RuleFor(x=>x.Name).MaximumLength(128).NotEmpty();
            RuleFor(x=>x.Interval).NotNull();
            RuleFor(x=>x.IsActive).NotNull();
            RuleFor(x=>x.Url).Matches(@"(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?").NotEmpty();
        }
    }
}