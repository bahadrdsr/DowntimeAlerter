using FluentValidation;

namespace Application.TargetApp.Commands.UpdateTargetApp
{
    public class UpdateTargetAppCommandValidator : AbstractValidator<UpdateTargetAppCommand>
    {
        public UpdateTargetAppCommandValidator()
        {
            RuleFor(x=>x.Name).MaximumLength(128).NotEmpty();
            RuleFor(x=>x.Interval).NotNull();
            RuleFor(x=>x.IsActive).NotNull();
            RuleFor(x=>x.Url).Matches(@"(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?").NotEmpty();
        }
    }
}