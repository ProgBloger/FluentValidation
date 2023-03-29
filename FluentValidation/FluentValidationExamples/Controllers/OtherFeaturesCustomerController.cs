using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators;
using FluentValidation.Results;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // Available languages https://github.com/FluentValidation/FluentValidation/tree/main/src/FluentValidation/Resources/Languages
    public class OtherFeaturesCustomerController : Controller
    {
        private IFactory _factory;
        public OtherFeaturesCustomerController(IFactory factory)
        {
            _factory = factory;
        }

        [HttpPost(Name = "OtherFeaturesCustomer")]
        public IActionResult PostCustomer(Customer customer)
        {
            var validator = _factory.Create<PreValidationCustomerValidator>();

            var validationResults = validator.Validate(customer);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }

        [HttpPut(Name = "OtherFeaturesCustomer")]
        public IActionResult PutCustomer()
        {
            var validator = _factory.Create<ComplexPersonValidator>();

            ValidationResult physicalValidationResults = ValidatePhysicalAddress(validator);
            ValidationResult virtualValidationResults = ValidateVirtualAddress(validator);

            if (!physicalValidationResults.IsValid || !virtualValidationResults.IsValid)
            {
                var common = physicalValidationResults.Errors.Union(virtualValidationResults.Errors);

                return BadRequest(common);
            }

            return Ok();
        }

        private static ValidationResult ValidateVirtualAddress(ComplexPersonValidator validator)
        {
            var virtualAddressPerson = new Person
            {
                Address = new VirtualAddress(),
                Addresses = new List<Models.Interfaces.IAddress>
                {
                    new VirtualAddress(),
                }
            };

            var virtualValidationResults = validator.Validate(virtualAddressPerson);
            return virtualValidationResults;
        }

        private static ValidationResult ValidatePhysicalAddress(ComplexPersonValidator validator)
        {
            var physicalAddressPerson = new Person
            {
                Address = new PhysicalAddress(),
                Addresses = new List<Models.Interfaces.IAddress>
                {
                    new PhysicalAddress(),
                }
            };

            var physicalValidationResults = validator.Validate(physicalAddressPerson);
            return physicalValidationResults;
        }
    }
}
