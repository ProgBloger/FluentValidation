//using FluentValidation;
//using FluentValidationExamples.Controllers;
//using FluentValidationExamples.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace FluentValidationTests
//{
//    [TestFixture]
//    public class RuleSetsCustomerControllerTests
//    {
//        private InlineValidator<Customer> _inlineValidator;
//        private RuleSetsCustomerController _sut;

//        [SetUp]
//        public void SetUp()
//        {
//            _inlineValidator = new InlineValidator<Customer>();
//            _sut = new RuleSetsCustomerController(_inlineValidator);
//        }

//        [Test]
//        public void PostCustomer_returns_BadRequest_when_NamesRequired_and_Surname_is_null()
//        {
//            // Arrange
//            _inlineValidator.RuleSet("NamesRequired", () =>
//            {
//                _inlineValidator.RuleFor(x => x.Surname).NotNull();
//            });
                
//            Customer customer = new Customer { Surname = null };

//            // Act
//            var result = _sut.PostCustomer(customer);

//            // Assert
//            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
//        }
//    }
//}
