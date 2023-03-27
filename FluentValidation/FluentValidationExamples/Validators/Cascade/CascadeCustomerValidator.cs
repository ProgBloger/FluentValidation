using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators.Cascade
{
    // https://docs.fluentvalidation.net/en/latest/cascade.html
    public class CascadeCustomerValidator : AbstractValidator<Customer>
    {
        public CascadeCustomerValidator()
        {
            RuleFor(customer => customer.Surname).NotNull().Equal("foo");

            RuleFor(customer => customer.Forename)
                .Cascade(CascadeMode.Stop)
                .NotNull().Equal("foo");
        }
    }
}
