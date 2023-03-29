using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators.Basics
{
    public class CollectionsIncludedCustomerValidator : AbstractValidator<Customer>
    {
        public CollectionsIncludedCustomerValidator(CollectionsCustomerValidator collectionsCustomerValidator)
        {
            RuleFor(customer => customer.Surname)
                .NotNull().NotEqual("string");

            Include(collectionsCustomerValidator);
        }
    }
}
