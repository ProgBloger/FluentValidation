using FluentValidation;
using FluentValidationExamples.Controllers;
using FluentValidationExamples.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationTests
{
    [TestFixture]
    public class RuleSetsPersonControllerTests
    {
        private InlineValidator<Person> _validator;
        private RuleSetsPersonController _sut;

        [SetUp]
        public void SetUp()
        {
            _validator = new InlineValidator<Person>();
            _sut = new RuleSetsPersonController(_validator);
        }

        [Test]
        public void PostCustomer_returns_BadRequest_when_NamesRequired_and_Surname_is_null()
        {
            // Arrange
            _validator.RuleSet("NamesRequired", () =>
            {
                _validator.RuleFor(x => x.Surname).NotNull();
            });

            var model = new Person { Surname = null };

            // Act
            var result = _sut.PersonPost(model);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}
