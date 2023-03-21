using FluentValidation;
using FluentValidation.Results;
using FluentValidationExamples.Extensions;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators.CustomValidators;

namespace FluentValidationExamples.Validators
{
    public class ConditionalCustomerValidator : AbstractValidator<Customer>
    {
        public ConditionalCustomerValidator()
        {
            RuleFor(customer => customer.CustomerDiscount)
                .GreaterThan(0).When(customer => customer.IsPreferredCustomer);

            RuleFor(customer => customer.CustomerDiscount)
                .Equal(0).Unless(customer => customer.IsPreferredCustomer);

            When(customer => customer.IsPreferredCustomer, () =>
            {
                RuleFor(customer => customer.CustomerDiscount).GreaterThan(0);
                RuleFor(customer => customer.CreditCardNumber).NotNull();
            });

            When(customer => customer.IsPreferredCustomer, () =>
            {
                RuleFor(customer => customer.CustomerDiscount).GreaterThan(0);
                RuleFor(customer => customer.CreditCardNumber).NotNull();
            }).Otherwise(() =>
            {
                RuleFor(customer => customer.CustomerDiscount).Equal(0);
            });

            // DependentRules
            RuleFor(x => x.Surname).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.Forename).NotNull();
            });


            // If you only want the condition to apply to the validator that immediately precedes the condition,
            // you must explicitly specify this:
            RuleFor(customer => customer.CustomerDiscount)
                .GreaterThan(0).When(customer => customer.IsPreferredCustomer, ApplyConditionTo.CurrentValidator)
                .Equal(0).When(customer => !customer.IsPreferredCustomer, ApplyConditionTo.CurrentValidator);


            // The first call to When applies to only the call to Matches, but not the call to NotEmpty
            // The second call to When applies only to the call to Empty.
            RuleFor(customer => customer.Photo)
                .NotEmpty()
                .Matches("https://www.photos.io/\\d+\\.png")
                .When(customer => customer.IsPreferredCustomer, ApplyConditionTo.CurrentValidator)
                .Empty()
                .When(customer => !customer.IsPreferredCustomer, ApplyConditionTo.CurrentValidator);


            RuleFor(customer => customer.Surname)
                .Null().WithMessage("{PropertyName}: {PropertyValue}")
                .NotNull().WithMessage("{PropertyName}: {PropertyValue}")

                // Is null or default
                .Empty().WithMessage("{PropertyName}: {PropertyValue}")

                // Not null, not empty string, not default value (0 for int)
                .NotEmpty().WithMessage("{PropertyName}: {PropertyValue}");


            RuleFor(customer => customer.Surname)
                .NotEqual("Foo")
                .NotEqual("Foo", StringComparer.OrdinalIgnoreCase)
                .NotEqual(customer => customer.Forename)
                .WithMessage("{PropertyName}: {PropertyValue}, {ComparisonProperty}: {ComparisonValue}");

            // !!! Not Equals but Equal. Equals is inherited from type Object
            RuleFor(customer => customer.Surname)
                .Equal("Foo")
                .Equal("Foo", StringComparer.OrdinalIgnoreCase)
                .Equal(customer => customer.CreditCardName)
                .WithMessage("{PropertyName}: {PropertyValue}, {ComparisonProperty}: {ComparisonValue}");

            RuleFor(customer => customer.Surname)
                .Length(1, 250)
                .WithMessage("{PropertyName}: {PropertyValue}, {TotalLength} not in {MinLength} - {MaxLength}")

                .MaximumLength(250)
                .WithMessage("{PropertyName}: {PropertyValue}, {TotalLength} exceeds {MaxLength}")

                .MinimumLength(10)
                .WithMessage("{PropertyName}: {PropertyValue}, {TotalLength} is less than {MinLength}");

            // Only valid on types that implement IComparable<T>
            RuleFor(customer => customer.CustomerDiscount)

                // Less
                .LessThan(100)
                .LessThan(customer => customer.MaxCustomerDiscount)
                .WithMessage("{PropertyName}: {PropertyValue}, {ComparisonProperty}: {ComparisonValue}")

                .LessThanOrEqualTo(100)
                .LessThanOrEqualTo(customer => customer.MaxCustomerDiscount)
                .WithMessage("{PropertyName}: {PropertyValue}, {ComparisonProperty}: {ComparisonValue}")

                // Greater
                .GreaterThan(100)
                .GreaterThan(customer => customer.MinCustomerDiscount)
                .WithMessage("{PropertyName}: {PropertyValue}, {ComparisonProperty}: {ComparisonValue}")

                .GreaterThanOrEqualTo(100)
                .GreaterThanOrEqualTo(customer => customer.MinCustomerDiscount)
                .WithMessage("{PropertyName}: {PropertyValue}, {ComparisonProperty}: {ComparisonValue}")

                .InclusiveBetween(1, 10)
                .WithMessage("{PropertyName}: {PropertyValue}, not in range {From} - {To}");

            // Email
            RuleFor(customer => customer.Email)
                // Checks only @ sign presence 
                .EmailAddress()
                // This is the same regex used by the EmailAddressAttribute in .NET 4.x
                // Deprecated
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex)
                .WithMessage("{PropertyName}: {PropertyValue}");

            // Enum
            // Only numbers present in enum can be assigned
            RuleFor(customer => customer.CustomerType)
                .IsInEnum()
                .WithMessage("{PropertyName}: {PropertyValue}");

            // Regex
            RuleFor(customer => customer.Surname)
                .Matches("some regex here \\d+")
                .WithMessage("{PropertyName}: {PropertyValue} did not match {RegularExpression}");

            // Predicates
            RuleFor(customer => customer.Surname)
                // Accepts Func<TProperty, bool> predicate
                .Must(surname => surname == "Foo")

                // Accepts Func<T, TProperty, bool> predicate
                .Must((customer, surname) => surname != customer.Forename)
                .WithMessage("{PropertyName}: {PropertyValue}");

            // Predicate example for custom validator
            RuleFor(customer => customer.Orders)
                .Must(orders => orders.Count < 10)
                .WithMessage("The list must contain fewer than 10 items");

            // Custom Validators
            RuleFor(customer => customer.Orders)
                .ListMustContainFewerThan(10)
                .ListMustContainFewerThan_WithCustomMessage(10)
                .Custom((list, context) =>
                {
                    if (list.Count > 10)
                    {
                        context.AddFailure("The list must contain 10 items or fewer");
                        context.AddFailure("DiscountProperty", "Can't have discount if list contains less than 10 items");
                        context.AddFailure(new ValidationFailure("DiscountProperty", "Can't have discount if list contains less than 10 items"));
                    }
                })
                .SetValidator(new ListCountValidator<Customer, Order>(10))
                .ListMustContainFewerThan_CustomValidator(10)
                .NotNull();

            // RootContextData
            // The RootContextData property is a Dictionary<string, object> available on the ValidationContext
            RuleFor(x => x.Surname).Custom((surname, context) =>
            {
                if (context.RootContextData.ContainsKey("MyCustomData"))
                {
                    context.AddFailure("My error message");
                }
            });

            Include(new CustomerCreditCardValidator());
            Include(new CustomerNameValidator());
        }

        protected override bool PreValidate(ValidationContext<Customer> context, ValidationResult result)
        {
            if(context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("", "Please ensure a model was supplied"));

                return false;
            }

            return true;
        }
    }
}
