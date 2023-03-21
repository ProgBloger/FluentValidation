using FluentValidation;
using FluentValidation.Results;
using FluentValidationExamples.Extensions;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators.CustomValidators;
using FluentValidationExamples.Validators.SupportExamples;

namespace FluentValidationExamples.Validators.Customability
{
    public class CustomCustomerValidator : AbstractValidator<Customer>
    {
        public CustomCustomerValidator()
        {
            // Predicates
            RuleFor(customer => customer.Surname)
                .Must(surname => surname == "Foo")

                // With model (customer)
                .Must((customer, surname) => surname != customer.Forename)
                .WithMessage("{PropertyName}: {PropertyValue}");

            RuleFor(customer => customer.Orders)
                .Must(orders => orders.Count < 10)
                .WithMessage("The list must contain fewer than 10 items");

            // Custom Validators
            RuleFor(customer => customer.Orders)
                .Custom((list, context) =>
                {
                    if (list.Count > 10)
                    {
                        context.AddFailure("The list must contain 10 items or fewer");
                        context.AddFailure("DiscountProperty", "Can't have discount if list contains less than 10 items");
                        context.AddFailure(new ValidationFailure("DiscountProperty", "Can't have discount if list contains less than 10 items"));
                    }

                    // RootContextData
                    // The RootContextData property is a Dictionary<string, object> available on the ValidationContext
                    var shouldFailNoMatterWhat = (bool)context.RootContextData["ShouldFailNoMatterWhat"];

                    if (shouldFailNoMatterWhat)
                    {
                        context.AddFailure("You shall not pass!!!");
                    }
                })
                .ListMustContainFewerThan(10)
                .ListMustContainFewerThan_WithCustomMessage(10)
                .SetValidator(new ListCountValidator<Customer, Order>(10))
                .ListMustContainFewerThan_CustomValidator(10)
                .NotNull();

            Include(new CustomerCreditCardValidator());
            Include(new CustomerNameValidator());
        }
    }
}
