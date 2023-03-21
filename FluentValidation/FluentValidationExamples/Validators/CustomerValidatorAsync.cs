using FluentValidation;
using System.Collections.Generic;
using FluentValidationExamples.ExternalClients;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Validators
{
    public class CustomerValidatorAsync : AbstractValidator<Customer>
    {
        private SomeExternalWebApiClient _client;

        public CustomerValidatorAsync(SomeExternalWebApiClient client)
        {
            _client = client;

            RuleFor(customer => customer.Id)
                .MustAsync(async (id, cancellationToken) =>
                {
                    bool exists = await _client.IdExists(id);
                    return !exists;
                })
                .WithMessage("ID Must be unique");

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

            RuleFor(customer => customer.Surname)
                .NotEqual("string")
                .WhenAsync(async (customer, ct) =>
                {
                    var clientCanBeString = await _client.ClientCanBeString(customer.Id);

                    return !clientCanBeString;
                });
        }
    }
}
