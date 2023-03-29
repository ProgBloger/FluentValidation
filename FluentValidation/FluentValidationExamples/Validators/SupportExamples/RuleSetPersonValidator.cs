using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators.SupportExamples
{
    public class RuleSetPersonValidator : AbstractValidator<Person>
    {
        public RuleSetPersonValidator()
        {
            RuleSet("NamesRequired", () =>
            {
                RuleFor(c => c.Surname).NotNull();
            });
        }
    }
}
