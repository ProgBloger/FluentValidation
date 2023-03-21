using FluentValidation;
using FluentValidation.Validators;

namespace FluentValidationExamples.Validators.CustomValidators
{
    public class ListCountValidator<T, TCollectionElement> : PropertyValidator<T, IList<TCollectionElement>>
    {
        private int _max;

        public ListCountValidator(int max)
        {
            _max = max;
        }

        public override bool IsValid(ValidationContext<T> context, IList<TCollectionElement> list)
        {
            if (list != null && list.Count >= _max)
            {
                context.MessageFormatter.AppendArgument("MaxElements", _max);
                return false;
            }

            return true;
        }

        public override string Name => "ListCountValidator";

        protected override string GetDefaultMessageTemplate(string errorCode) => "{PropertyName} must contain fewer than {MaxElements} items.";
    }
}
