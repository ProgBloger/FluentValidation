using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        // Complete error message reference
        // https://docs.fluentvalidation.net/en/latest/built-in-validators.html
        public UserValidator()
        {
            RuleFor(user => user.Surname).NotNull()
                .WithMessage("Please ensure you have entered your {PropertyName}")

                .MaximumLength(10)
                .WithMessage("Length is {TotalLength}. Maximum length allwed: {MaxLength}")

                .MinimumLength(8)
                .WithMessage("Length is {TotalLength}. Minimum length allwed: {MinLength}");

            RuleFor(user => user.Cash).LessThan(user => user.Credits)
                .WithMessage("User {PropertyName} is {PropertyValue} and is not less than {ComparisonProperty}: {ComparisonValue}");

            RuleFor(user => user.Password).MinimumLength(8)
                .WithMessage("The password for user {user.Surname} is too short");
        }
    }
}
