using FluentValidation;
using Microsoft.Extensions.Localization;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class LocalizationCustomerValidator : AbstractValidator<Customer>
    {
        private IStringLocalizer<LocalizationCustomerValidator> _localizer;

        public LocalizationCustomerValidator(IStringLocalizer<LocalizationCustomerValidator> localizer)
        {
            _localizer = localizer;

            RuleFor(c => c.Forename)
                .NotEqual("string");

            RuleFor(c => c.Surname)
                .NotEqual("string")
                .WithName(_localizer["Surname"]);

            RuleFor(c => c.Email)
                .NotNull();
        }
    }
}
