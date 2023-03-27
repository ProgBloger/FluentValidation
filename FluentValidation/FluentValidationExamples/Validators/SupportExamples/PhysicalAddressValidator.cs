using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators.SupportExamples
{
    public class PhysicalAddressValidator : AbstractValidator<PhysicalAddress>
    {
        public PhysicalAddressValidator()
        {
            RuleFor(x => x.Address1)
                .NotNull();

            RuleFor(x => x.Address2)
                .NotNull();

            RuleFor(x => x.ZipCode)
                .NotNull();
        }
    }
}
