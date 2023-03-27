using FluentValidation;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators.Basics;
using FluentValidationExamples.Validators.Customability;
using FluentValidationExamples.Validators.Customization;

namespace FluentValidationExamples.Validators.SupportExamples
{
    public class SpecificPropertiesValidator : AbstractValidator<Customer>
    {
        public SpecificPropertiesValidator(IFactory factory)
        {
            var collectionsCustomerValidator = factory.Create<CollectionsCustomerValidator>();
            var defaultValidatorsCustomerValidator = factory.Create<DefaultValidatorsCustomerValidator>();
            var conditionalCustomerValidator = factory.Create<ConditionalCustomerValidator>();
            var customizedCustomerValidator = factory.Create<CustomizedCustomerValidator>();
            
            Include(collectionsCustomerValidator);
            Include(defaultValidatorsCustomerValidator);
            Include(conditionalCustomerValidator);
            Include(customizedCustomerValidator);
        }
    }
}
