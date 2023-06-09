﻿using FluentValidation;
using FluentValidation.Results;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators.Basics
{
    public class ExceptionCustomerValidator : AbstractValidator<Customer>
    {
        public ExceptionCustomerValidator()
        {
            Include(new CustomerValidator());
        }

        //https://docs.fluentvalidation.net/en/latest/advanced.html?highlight=RaiseValidationException#customizing-the-validation-exception
        protected override void RaiseValidationException(ValidationContext<Customer> context, ValidationResult result)
        {
            var ex = new ValidationException(result.Errors);

            throw new ArgumentException(ex.Message, ex);
        }
    }
}
