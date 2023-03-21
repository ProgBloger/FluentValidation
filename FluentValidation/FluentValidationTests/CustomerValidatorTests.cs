using FluentValidationExamples.Models;
using FluentValidationExamples.Validators;
using FluentValidation.TestHelper;
using FluentValidation;
using FluentValidation.Results;

namespace FluentValidationTests
{
    /// <summary>
    /// Documentation on tests can be found here
    /// https://docs.fluentvalidation.net/en/latest/testing.html
    /// 
    /// Documentation suggests assertation and mocking to be made
    /// only by using the FluentValidation extension methods
    /// 
    /// Extension methods from the TestHelper namespace can be found here
    /// https://github.com/FluentValidation/FluentValidation/blob/main/src/FluentValidation/TestHelper/TestValidationResult.cs
    /// https://github.com/FluentValidation/FluentValidation/blob/main/src/FluentValidation/TestHelper/ValidatorTestExtensions.cs
    /// </summary>
    [TestFixture]
    public class CustomerValidatorTests
    {
        private CustomerValidator validator;
        
        [SetUp]
        public void Setup()
        {
            validator = new CustomerValidator();
        }

        [Test]
        public void Should_have_error_when_Name_is_string()
        {
            // Arrange
            var model = new Customer { Surname = "string", Orders = new List<Order>() };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(customer => customer.Surname);
        }

        [Test]
        public void Should_not_have_error_when_correct_Surname_is_specified()
        {
            // Arrange
            var model = new Customer { Surname = "Elon", Orders = new List<Order>() };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(customer => customer.Surname);
        }

        [Test]
        public void Should_have_property_and_collection_error_when_Surname_is_string_and_total_zero()
        {
            // Arrange
            var model = new Customer { Surname = "string", Orders = new List<Order> { new Order { Total = 0 } } };

            // Act
            var result = validator.TestValidate(model);

            // Assert

            // Validation error for the Surname property
            result.ShouldHaveValidationErrorFor(customer => customer.Surname);
            
            // No validation error for the Forename property
            result.ShouldNotHaveValidationErrorFor(x => x.Forename);

            // Validation error for the Total property in the first element of Orders
            // You can also use a string name for properties that can't be easily represented with a lambda, eg:
            result.ShouldHaveValidationErrorFor("Orders[0].Total");
        }

        [Test]
        public void Should_have_particular_error_when_Surname_is_null()
        {
            // Arrange
            var model = new Customer { Surname = null, Orders = new List<Order>() };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(customer => customer.Surname)
                .WithErrorMessage("'Surname' must not be empty.")
                .WithSeverity(Severity.Error)
                .WithErrorCode("NotNullValidator");
        }

        [Test]
        public void Should_have_one_error_only_when_Surname_is_null()
        {
            // Arrange
            var model = new Customer { Surname = null, Orders = new List<Order>() };

            // Act
            var result = validator.TestValidate(model);

            // Assert

            // Failures happen for the Surname property only
            result.ShouldHaveValidationErrorFor(customer => customer.Surname)
                .Only();

            // The only message is the specified one
            result.ShouldHaveValidationErrorFor(customer => customer.Surname)
                .WithErrorMessage("'Surname' must not be empty.")
                .Only();

            // Availabe methods:
                // .WithoutErrorMessage()
                // .WithoutErrorCode()
                // .WithoutSeverity()
                // .WithoutCustomState()
        }
    }
}