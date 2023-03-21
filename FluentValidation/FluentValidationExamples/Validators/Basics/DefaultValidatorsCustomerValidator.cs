using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators.Basics
{
    // https://docs.fluentvalidation.net/en/latest/built-in-validators.html
    public class DefaultValidatorsCustomerValidator : AbstractValidator<Customer>
    {
        public DefaultValidatorsCustomerValidator()
        {

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
        }
    }
}
