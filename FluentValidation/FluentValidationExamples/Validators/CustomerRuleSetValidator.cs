using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class CustomerRuleSetValidator : AbstractValidator<Customer>
    {
        public CustomerRuleSetValidator()
        {
            RuleSet("NamesRequired", () =>
            {
                RuleFor(x => x.Surname).NotNull();
                RuleFor(x => x.Forename).NotNull();
            });

            RuleSet("NotElonMusk", () =>
            {
                RuleFor(x => x.Surname).NotEqual("Elon");
                RuleFor(x => x.Forename).NotEqual("Musk");
            });

            RuleFor(x => x.Id).NotEqual(0);
        }
    }
}
