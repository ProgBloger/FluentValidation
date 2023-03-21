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

namespace FluentValidationTests
{
    [TestFixture]
    public class CustomerValidatorAsyncTests
    {
        private CustomerValidatorAsync _validator;

        [SetUp]
        public void Setup()
        {
            var client = new SomeExternalWebApiClient();
            _validator = new CustomerValidatorAsync(client);
        }

        [Test]
        public async Task Shoul_have_error_when_ID_not_unique()
        {
            // Arrange
            var model = new Customer { Surname = "string", Orders = new List<Order>() };

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(customer => customer.Id)
                .WithErrorMessage("ID Must be unique");
        }
    }
}
