using FluentValidation;
using FluentValidation.Results;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class ExceptionCustomerValidator : AbstractValidator<Customer>
    {
        public ExceptionCustomerValidator()
        {
            Include(new CustomerValidator());
        }

        protected override void RaiseValidationException(ValidationContext<Customer> context, ValidationResult result)
        {
            var ex = new ValidationException(result.Errors);

            throw new ArgumentException(ex.Message, ex);
        }
    }
}
