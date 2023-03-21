﻿using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class CustomerCreditCardValidator : AbstractValidator<Customer>
    {
        public CustomerCreditCardValidator()
        {
            RuleFor(x => x.CreditCardName)
                .Must(NotBeElon);
        }

        public Func<string, bool> NotBeElon => (arg) => !arg.ToLower().Contains("elon");
    }
}
