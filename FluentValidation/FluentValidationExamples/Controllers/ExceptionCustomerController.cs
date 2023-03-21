using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators;
using FluentValidation;
using FluentValidationExamples.Extensions;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExceptionCustomerController : Controller
    {
        private IFactory _factory;
        public ExceptionCustomerController(IFactory factory)
        {
            _factory = factory;
        }

        [HttpPost(Name = "ExceptionCustomer")]
        public IActionResult PostCustomer(Customer customer)
        {
            var validator = _factory.Create<CustomerValidator>();

            // The lines below are equivalent and require FluentValidation using
            validator.ValidateAndThrow(customer);

            //var validationResults = _validator.Validate(customer,
            //    options => options.ThrowOnFailures());

            return Ok();
        }

        [HttpPut(Name = "ExceptionCustomer")]
        public IActionResult PutCustomer(Customer customer)
        {
            var validator = _factory.Create<ExceptionCustomerValidator>();

            validator.ValidateAndThrow(customer);

            return Ok();
        }

        [HttpDelete(Name = "ExceptionCustomer")]
        public IActionResult DeleteCustomer(Customer customer)
        {
            var validator = _factory.Create<CustomerValidator>();

            validator.ValidateAndThrowArgumentException(customer);

            return Ok();
        }
    }
}
