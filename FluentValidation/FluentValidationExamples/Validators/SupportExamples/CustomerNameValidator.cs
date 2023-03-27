using FluentValidation;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators.SupportExamples
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

            if (arg == null)
                return true;

            return arg.ToLower() != elon;
        }
    }
}
