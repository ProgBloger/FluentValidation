using FluentValidation;
using FluentValidationExamples.ExternalClients;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    // https://docs.fluentvalidation.net/en/latest/async.html
    public class AsynchronousCustomerValidator : AbstractValidator<Customer>
    {
        private SomeExternalWebApiClient _client;

        public AsynchronousCustomerValidator(SomeExternalWebApiClient client)
        {
            _client = client;

            RuleSet("MustAsync", () =>
            {
                RuleFor(customer => customer.Id)
                    .MustAsync(async (id, cancellationToken) =>
                    {
                        bool exists = await _client.IdExists(id);
                        return !exists;
                    })
                    .WithMessage("ID Must be unique");
            });

            RuleSet("CustomAsync", () =>
            {
                RuleFor(customer => customer.Surname)
                    .NotEqual("string")
                    .CustomAsync(async (surname, context, ct) =>
                    {
                        var clientCanBeString = await _client.ClientCanBeString(context.InstanceToValidate.Id);

                        if (!clientCanBeString)
                        {
                            context.AddFailure("\"string\" is a default value. Try some other name");
                        }
                    });
            });

            RuleSet("WhenAsync", () =>
            {
                RuleFor(customer => customer.Surname)
                    .NotEqual("string")
                    .WhenAsync(async (customer, ct) =>
                    {
                        var clientCanBeString = await _client.ClientCanBeString(customer.Id);

                        return !clientCanBeString;
                    });
            });
        }
    }
}
