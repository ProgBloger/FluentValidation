using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators;
using FluentValidation;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // https://docs.fluentvalidation.net/en/latest/async.html
    public class AsynchronousCustomerController : Controller
    {
        private IFactory _factory;
        public AsynchronousCustomerController(IFactory factory)
        {
            _factory = factory;
        }

        [HttpPost(Name = "AsynchronousCustomer")]
        public async Task<IActionResult> CustomerPost(Customer customer)
        {
            var validator = _factory.Create<AsynchronousCustomerValidator>();

            var validationResults = await validator.ValidateAsync(customer, options =>
            {
                options.IncludeRuleSets("MustAsync");
            });

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }

        [HttpPut(Name = "AsynchronousCustomer")]
        public async Task<IActionResult> CustomerPut(Customer customer)
        {
            var validator = _factory.Create<AsynchronousCustomerValidator>();

            var validationResults = await validator.ValidateAsync(customer, options =>
            {
                options.IncludeRuleSets("CustomAsync");
            });

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }

        [HttpDelete(Name = "AsynchronousCustomer")]
        public async Task<IActionResult> CustomerDelete(Customer customer)
        {
            var validator = _factory.Create<AsynchronousCustomerValidator>();

            var validationResults = await validator.ValidateAsync(customer, options =>
            {
                options.IncludeRuleSets("WhenAsync");
            });

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }
    }
}
