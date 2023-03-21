using FluentValidation.Results;
using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class AdvancedCustomerValidator : AbstractValidator<Customer>
    {
        public AdvancedCustomerValidator()
        {
            RuleFor(x => x.Surname).Custom((surname, context) =>
            {
                if (context.RootContextData.ContainsKey("MyCustomData"))
                {
                    context.AddFailure("My error message");
                }
            });
        }
    }
}
