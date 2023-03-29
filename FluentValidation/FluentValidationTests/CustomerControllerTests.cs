using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Controllers;
using FluentValidationExamples.Models;

namespace FluentValidationTests
{
    // https://docs.fluentvalidation.net/en/latest/testing.html#mocking
    // Validators are intended to be “black boxes” and we don’t generally recommend mocking them.
    // Within a test, the recommended approach is to supply a real validator instance with known bad data 
    // in order to trigger a validation error.
    // We strongly recommend not using a mocking library.

    [TestFixture]
    public class CustomerControllerTests
    {
        private InlineValidator<Customer> _inlineValidator;
        private CustomerController _sut;

        [SetUp]
        public void SetUp()
        {
            _inlineValidator = new InlineValidator<Customer>();
            _sut = new CustomerController(_inlineValidator);
        }

        [Test]
        public void Customer_returns_BadRequest_when_Surname_is_null()
        {
            // Arrange
            var customer = new Customer { Surname = null, Orders = new List<Order>() };
            
            _inlineValidator.RuleFor(c => c.Surname)
                .NotNull();

            // Act
            var result = _sut.Customer(customer);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}
