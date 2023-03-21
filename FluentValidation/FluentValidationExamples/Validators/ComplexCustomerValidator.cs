using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class ComplexCustomerValidator : AbstractValidator<Person>
    {
        public ComplexCustomerValidator()
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
