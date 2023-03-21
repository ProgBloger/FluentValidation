﻿using FluentValidation;
using FluentValidation.Results;
using FluentValidationExamples.Extensions;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators.CustomValidators;

namespace FluentValidationExamples.Validators
{
    public class TRConditionalCustomerValidator : AbstractValidator<Customer>
    {
        public TRConditionalCustomerValidator()
        {
            RuleFor(customer => customer.CustomerDiscount)
                .GreaterThan(0)
                .When(customer => customer.IsPreferredCustomer);

            RuleFor(customer => customer.CustomerDiscount)
                .Equal(0)
                .Unless(customer => customer.IsPreferredCustomer);

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


            // DependentRules
            RuleFor(x => x.Surname).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.Forename).NotNull();
            });
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