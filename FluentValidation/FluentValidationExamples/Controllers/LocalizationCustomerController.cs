using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // Available languages https://github.com/FluentValidation/FluentValidation/tree/main/src/FluentValidation/Resources/Languages
    public class LocalizationCustomerController : Controller
    {
        private IFactory _factory;
        public LocalizationCustomerController(IFactory factory)
        {
            _factory = factory;
        }

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
    }
}
