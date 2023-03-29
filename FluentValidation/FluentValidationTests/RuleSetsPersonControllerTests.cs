using FluentValidation;
using FluentValidationExamples;
using FluentValidationExamples.Controllers;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators.Customization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

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
