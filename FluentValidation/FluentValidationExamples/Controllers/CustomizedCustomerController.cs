using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidation;
using FluentValidationExamples.Validators.Customability;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomizedCustomerController : Controller
    {
        private IFactory _factory;
        public CustomizedCustomerController(IFactory factory)
        {
            _factory = factory;
        }

        [HttpPost(Name = "CustomizedCustomer")]
        public IActionResult PostCustomer(Customer customer)
        {
            var validator = _factory.Create<CustomizedCustomerValidator>();

            var validationResults = validator.Validate(customer);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }

        [HttpPut(Name = "CustomizedCustomer")]
        public IActionResult PutCustomer(Customer customer)
        {
            var validator = _factory.Create<CustomizedCustomerValidator>();

            // Passing data to validator
            var context = new ValidationContext<Customer>(customer);
            context.RootContextData["ShouldFailNoMatterWhat"] = true;
            var validationResults = validator.Validate(context);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }
    }
}
