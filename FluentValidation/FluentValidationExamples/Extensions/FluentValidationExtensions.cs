using FluentValidation;
using FluentValidationExamples.Validators.CustomValidators;

namespace FluentValidationExamples.Extensions
{
    public static class FluentValidationExtensions
    {
        #region CustomValidationExtensions
        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        {
            return ruleBuilder.Must(list => list.Count < num).WithMessage("The list must contain fewer elements");
        }

        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThan_WithCustomMessage<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        {
            return ruleBuilder.Must((rootObject, list, context) =>
            {
                context.MessageFormatter
                    .AppendArgument("MaxElements", num)
                    .AppendArgument("TotalElements", list.Count);

                return list.Count < num;
            })
            .WithMessage("{PropertyName} must contain fewer than {MaxElements} items. The list contains {TotalElements} elements.");
        }

        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThan_CustomValidator<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        {
            return ruleBuilder.SetValidator(new ListCountValidator<T, TElement>(num));
        }

        #endregion

        public static void ValidateAndThrowArgumentException<T>(this IValidator<T> validator, T instance)
        {
            var res = validator.Validate(instance);

            if (!res.IsValid)
            {
                var ex = new ValidationException(res.Errors);

                throw new ArgumentException(ex.Message, ex);
            }
        }
    }
}
