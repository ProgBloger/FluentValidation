using FluentValidation;
using FluentValidationExamples.Models;
using System.Collections.Generic;

namespace FluentValidationExamples.Validators.Basics
{
    // https://docs.fluentvalidation.net/en/latest/collections.html#
    public class CollectionsCustomerValidator : AbstractValidator<Customer>
    {
        public CollectionsCustomerValidator()
        {
            RuleForEach(x => x.Orders).SetValidator(new OrderValidator());

            // Selecting items from the list that should be validated
            RuleForEach(x => x.Orders)
                .Where(order => order.Sum != null)
                .SetValidator(new OrderValidator());

            RuleForEach(x => x.Orders).ChildRules(orders =>
            {
                orders.RuleFor(x => x.Total).LessThan(1000);
            });

            // Applying validation rules to each item after the collection rule
            RuleFor(x => x.Orders)
                .Must(x => x.Count < 10)
                    .WithMessage("Amount of orders can't be more than 10")
                .ForEach(orderRule =>
                {
                    orderRule.Must(order => order.Total > 0)
                        .WithMessage("Orders must have a total of more that 0");
                });
        }
    }
}
