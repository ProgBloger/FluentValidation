using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidation;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // Creating your first validator
    // https://docs.fluentvalidation.net/en/latest/start.html
    public class CustomerController : Controller
    {
        private IValidator<Customer> _validator;

        public CustomerController(IValidator<Customer> validator)
        {
            _validator = validator;
        }

        [HttpPost(Name = "Customer")]
        public IActionResult Customer(Customer customer)
        {
            var validationResults = _validator.Validate(customer);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }
    }
}
