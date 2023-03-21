using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentValidationExamples;
using FluentValidationExamples.Controllers;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators.Basics;

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
        private CustomerValidator _validator;
        private Mock<IFactory> _factoryMock;
        private CustomerController _sut;

        [SetUp]
        public void SetUp()
        {
            _validator = new CustomerValidator();
            
            _factoryMock = new Mock<IFactory>();
            _factoryMock.Setup(m => m.Create<IValidator<Customer>>())
                .Returns(_validator);

            //_sut = new CustomerController(_factoryMock.Object);
        }

        [Test]
        public void Customer_returns_BadRequest_when_Surname_is_null()
        {
            // Arrange
            var customer = new Customer { Surname = null, Orders = new List<Order>() };

            // Act
            var result = _sut.Customer(customer);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}
