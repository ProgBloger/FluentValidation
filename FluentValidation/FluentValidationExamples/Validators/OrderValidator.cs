using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Total).GreaterThan(0);
            RuleForEach(x => x.Tags).NotNull().WithMessage("Tag {CollectionIndex} is required.");
        }
    }
}
