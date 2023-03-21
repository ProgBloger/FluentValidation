using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators;
using FluentValidation;
using System.Globalization;
using FluentValidationExamples.Validators.Basics;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocalizationCustomerController : Controller
    {
        private IFactory _factory;
        public LocalizationCustomerController(IFactory factory)
        {
            _factory = factory;
        }

        // Available languages https://github.com/FluentValidation/FluentValidation/tree/main/src/FluentValidation/Resources/Languages
        [HttpPost(Name = "LocalizationCustomer")]
        public IActionResult PostCustomer(Customer customer)
        {
            var validator = _factory.Create<LocalizationCustomerValidator>();

            var validationResults = validator.Validate(customer);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }

        [HttpPut(Name = "LocalizationCustomer")]
        public IActionResult PutCustomer(Customer customer)
        {
            var validator = _factory.Create<CustomerValidator>();

            var validationResults = validator.Validate(customer);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }

        [HttpDelete(Name = "LocalizationCustomer")]
        public IActionResult DeleteCustomer(Customer customer)
        {
            var validator = _factory.Create<CustomerValidator>();

            var validationResults = validator.Validate(customer);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }
    }
}
