using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators.Cascade
{
    // https://docs.fluentvalidation.net/en/latest/cascade.html
    // Uses the Fail Fast strategy
    // The only error will appear is from the first Validator of the first Rule
    public class ClassLevelCascadeCustomerValidator : AbstractValidator<Customer>
    {
        public ClassLevelCascadeCustomerValidator()
        {
            // You can also set Class Level globally in Program.cs
            // By setting ValidatorOptions.Global.DefaultClassLevelCascadeMode to Stop
            this.ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(customer => customer.Surname).NotNull().Equal("foo");
            RuleFor(customer => customer.Forename).NotNull().Equal("foo");
            RuleFor(customer => customer.CreditCardName).NotNull().Equal("foo");
        }
    }
}
