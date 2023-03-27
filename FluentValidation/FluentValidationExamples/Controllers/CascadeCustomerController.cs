using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators.Cascade;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // https://docs.fluentvalidation.net/en/latest/cascade.html
    public class CascadeCustomerController : Controller
    {
        private IFactory _factory;
        public CascadeCustomerController(IFactory factory)
        {
            _factory = factory;
        }

        [HttpPost(Name = "CascadeCustomer")]
        public IActionResult CustomerPost(Customer customer)
        {
            var validator = _factory.Create<CascadeCustomerValidator>();

            var validationResults = validator.Validate(customer);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }

        [HttpPut(Name = "CascadeCustomer")]
        public IActionResult CustomerPut(Customer customer)
        {
            var validator = _factory.Create<RuleLevelCascadeCustomerValidator>();

            var validationResults = validator.Validate(customer);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }

        [HttpDelete(Name = "CascadeCustomer")]
        public IActionResult CustomerDelete(Customer customer)
        {
            var validator = _factory.Create<ClassLevelCascadeCustomerValidator>();

            var validationResults = validator.Validate(customer);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }
    }
}
