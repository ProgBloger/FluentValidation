using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class VirtualAddressValidator : AbstractValidator<VirtualAddress>
    {
        public VirtualAddressValidator()
        {
            RuleFor(x => x.Address1)
                .NotNull();

            RuleFor(x => x.Address2)
                .NotNull();

            RuleFor(x => x.Expires)
                .GreaterThan(DateTime.Now);
        }
    }
}
