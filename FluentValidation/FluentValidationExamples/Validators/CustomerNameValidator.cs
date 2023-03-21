using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class CustomerNameValidator : AbstractValidator<Customer>
    {
        public CustomerNameValidator()
        {
            RuleFor(x => x.Surname).Must(NotBeElon);
        }

        private bool NotBeElon(string arg)
        {
            string elon = "elon";

            return arg.ToLower() != elon;
        }
    }
}
