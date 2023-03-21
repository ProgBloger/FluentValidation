using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class InfoCustomerValidator : AbstractValidator<Customer>
    {
        public InfoCustomerValidator()
        {
            // Severity. Severity.Error is the default value
            RuleFor(customer => customer.Surname).NotEqual("string").WithSeverity(Severity.Warning);
            RuleFor(customer => customer.CreditCardName).NotEqual("string").WithSeverity(Severity.Info);

            // Custom State
            RuleFor(customer => customer.Email)
                .NotEqual("string")
                .WithState(customer => new { Id = new Guid(), Name = customer.Surname });

            // ErrorCode
            // Default error code is NotNullValidator
            RuleFor(customer => customer.Forename).NotNull();
            RuleFor(customer => customer.Surname).NotNull().WithErrorCode("ERR1234");

            // If the ErrorCode is set to one of the default validators
            // The error message will also be changed
            RuleFor(customer => customer.CreditCardName).Must(creditCardName =>
            {
                return creditCardName != null;
                // Will result to the "'Credit Card Name' must not be empty." error message
            }).WithErrorCode("NotNullValidator");
            
        }
    }
}
