using FluentValidation;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators.SupportExamples;

namespace FluentValidationExamples.Validators
{
    public class ComplexPersonValidator : AbstractValidator<Person>
    {
        public ComplexPersonValidator()
        {
            RuleFor(x => x.Address).SetInheritanceValidator(v =>
            {
                v.Add<PhysicalAddress>(new PhysicalAddressValidator());
                v.Add<VirtualAddress>(new VirtualAddressValidator());
            });

            RuleForEach(x => x.Addresses).SetInheritanceValidator(v =>
            {
                v.Add<PhysicalAddress>(new PhysicalAddressValidator());
                v.Add<VirtualAddress>(new VirtualAddressValidator());
            });
        }

    }
}
