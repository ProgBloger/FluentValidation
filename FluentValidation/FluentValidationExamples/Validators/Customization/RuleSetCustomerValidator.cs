using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators.Customization
{
    // https://docs.fluentvalidation.net/en/latest/rulesets.html
    public class RuleSetCustomerValidator : AbstractValidator<Customer>
    {
        public RuleSetCustomerValidator()
        {
            RuleSet("NamesRequired", () =>
            {
                RuleFor(x => x.Surname).NotNull();
                RuleFor(x => x.Forename).NotNull();
            });

            RuleSet("CustomerDiscount", () =>
            {
                RuleFor(x => x.MaxCustomerDiscount).GreaterThan(10);
                RuleFor(x => x.MinCustomerDiscount).GreaterThanOrEqualTo(0);
            });

            RuleFor(x => x.Id).NotEqual(0);
        }
    }
}
