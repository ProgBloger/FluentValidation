using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidation;
using FluentValidationExamples.Validators.Customization;
using FluentValidationExamples.Validators.SupportExamples;
using FluentValidationExamples.Validators.Basics;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // https://docs.fluentvalidation.net/en/latest/specific-properties.html
    public class SpecificPropertiesCustomerController : Controller
    {
        private IFactory _factory;
        public SpecificPropertiesCustomerController(IFactory factory)
        {
            _factory = factory;
        }

        [HttpPost(Name = "SpecificPropertiesCustomer")]
        // Splitted validation for the example purpose
        public IActionResult CustomerPost(Customer customer)
        {
            var validator = _factory.Create<SpecificPropertiesValidator>();

            var validationResult = validator.Validate(customer);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }

            return Ok();
        }

        [HttpPut(Name = "SpecificPropertiesCustomer")]
        public IActionResult CustomerPut(Customer customer)
        {
            var validator = _factory.Create<SpecificPropertiesValidator>();

            //Only CustomerType and Orders.Total will be validated
            var validationResult = validator.Validate(customer, options =>
            {
                options.IncludeProperties(x => x.CustomerType);
                options.IncludeProperties("Orders.Total");
            });

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return Ok();
        }
    }
}
