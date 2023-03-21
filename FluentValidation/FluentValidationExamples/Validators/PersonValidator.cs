using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleSet("NamesRequired", () =>
            {
                RuleFor(x => x.Surename).NotNull();
                RuleFor(x => x.Forename).NotNull();
            });

            RuleSet("NotElonMusk", () =>
            {
                RuleFor(x => x.Surename).NotEqual("Elon");
                RuleFor(x => x.Forename).NotEqual("Musk");
            });

            RuleFor(x => x.Id).NotEqual(0);
        }
    }
}
