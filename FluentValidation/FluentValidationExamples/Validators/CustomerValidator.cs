using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Surname).NotNull().NotEqual("string");

            // Collections
            RuleForEach(x => x.Orders).SetValidator(new OrderValidator());
            RuleForEach(x => x.Orders)
                .Where(order => order.Sum != null)
                .SetValidator(new OrderValidator());

            RuleForEach(x => x.Orders).ChildRules(orders =>
            {
                orders.RuleFor(x => x.Total).LessThan(1000);
            });

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
