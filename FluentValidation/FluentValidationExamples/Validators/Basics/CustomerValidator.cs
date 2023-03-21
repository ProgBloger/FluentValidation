using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators.Basics
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Surname)
                .NotNull().NotEqual("string");
        }
    }
}
