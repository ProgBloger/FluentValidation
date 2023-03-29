using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidationExamples.ExternalClients;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators;
using FluentValidation;
using FluentValidation.Internal;

namespace FluentValidationTests
{
    [TestFixture]
    public class CustomerValidatorAsyncTests
    {
        private AsynchronousCustomerValidator _validator;

        [SetUp]
        public void Setup()
        {
            var client = new SomeExternalWebApiClient();
            _validator = new AsynchronousCustomerValidator(client);
        }

        [Test]
        public async Task Shoul_have_error_when_RuleSet_is_MustAsync_and_ID_not_unique()
        {
            // Arrange
            const string expectedErrorMessage = "ID Must be unique";
            const string mustAsyncRuleSet = "MustAsync";

            Customer customer = new Customer { Orders = new List<Order>() };

            // Act
            var result = await _validator.TestValidateAsync(customer, options => { options.IncludeRuleSets(mustAsyncRuleSet); });

            // Assert
            result.ShouldHaveValidationErrorFor(customer => customer.Id)
                .WithErrorMessage(expectedErrorMessage);
        }
    }
}
