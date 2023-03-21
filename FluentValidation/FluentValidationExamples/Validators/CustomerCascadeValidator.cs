using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class CustomerCascadeValidator : AbstractValidator<Customer>
    {
        public CustomerCascadeValidator()
        {
            // NotEqual validation will fire after the NotNull even if NotNull failed
            RuleSet("DefaultCascadeBehaviour", () =>
            {
                RuleFor(customer => customer.Forename).NotEqual("string").Length(4, 5);
                RuleFor(customer => customer.CreditCardName).NotEqual("string").Length(4, 5);
                RuleFor(customer => customer.Surname).NotEqual("string").Length(4, 5);
            });

            // If the first validation fails the second won't be fired
            RuleSet("StopCascade", () =>
            {
                RuleFor(customer => customer.Surname).Cascade(CascadeMode.Stop).NotNull().NotEqual("string");
            });

            // Each rule will execute but each rull will stop at the first failure
            RuleSet("RuleLevelCascadeMode", () =>
            {
                //this.RuleLevelCascadeMode = CascadeMode.Stop;

                RuleFor(customer => customer.Forename).NotNull().NotEqual("string");
                RuleFor(customer => customer.CreditCardName).NotNull().NotEqual("string");
                RuleFor(customer => customer.Surname).NotNull().NotEqual("string");
            });

            // If the first rule fails other rules won't execute
            RuleSet("ClassLevelCascadeMode", () =>
            {
                //this.ClassLevelCascadeMode = CascadeMode.Stop;

                RuleFor(customer => customer.Forename).NotNull().NotEqual("string");
                RuleFor(customer => customer.CreditCardName).NotNull().NotEqual("string");
                RuleFor(customer => customer.Surname).NotNull().NotEqual("string");
            });

            RuleFor(x => x.Surname).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.Forename).NotNull();
            });
        }
    }
}
