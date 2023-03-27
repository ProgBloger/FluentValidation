using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators.Cascade
{
    // https://docs.fluentvalidation.net/en/latest/cascade.html
    public class RuleLevelCascadeCustomerValidator : AbstractValidator<Customer>
    {
        public RuleLevelCascadeCustomerValidator()
        {
            // Instead of adding .Cascade(CascadeMode.Stop) to each Rule
            // we set this property to CascadeMode.Stop;
            // You can also set Rule Level globally in Program.cs
            // By setting ValidatorOptions.Global.DefaultRuleLevelCascadeMode to Stop
            this.RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(customer => customer.Surname).NotNull().Equal("foo");
            RuleFor(customer => customer.Forename).NotNull().Equal("foo");
            RuleFor(customer => customer.CreditCardName).NotNull().Equal("foo");

            //RuleFor(customer => customer.Surname).Cascade(CascadeMode.Stop).NotNull().Equal("foo");
            //RuleFor(customer => customer.Forename).Cascade(CascadeMode.Stop).NotNull().Equal("foo");
            //RuleFor(customer => customer.CreditCardName).Cascade(CascadeMode.Stop).NotNull().Equal("foo");
        }
    }
}
