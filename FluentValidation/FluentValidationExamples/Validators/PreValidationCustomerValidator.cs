using FluentValidation;
using FluentValidation.Results;
using FluentValidationExamples.Extensions;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators.CustomValidators;

namespace FluentValidationExamples.Validators
{
    public class PreValidationCustomerValidator : AbstractValidator<Customer>
    {
        public PreValidationCustomerValidator()
        {
            RuleFor(customer => customer.Surname)
                .NotNull();
        }

        protected override bool PreValidate(ValidationContext<Customer> context, ValidationResult result)
        {
            if(context.InstanceToValidate.Id == 0)
            {
                result.Errors.Add(new ValidationFailure("", "Please, provide Id for the customer"));

                return false;
            }

            return true;
        }
    }
}
