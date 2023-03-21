using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators;
using FluentValidation;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private IFactory _factory;
        public CustomerController(IFactory factory)
        {
            _factory = factory;
        }

        [HttpPost(Name = "Customer")]
        public IActionResult Customer(Customer customer)
        {
            var validator = _factory.Create<CustomerValidator>();

            #region ValidateSpecificPropertiesOnly
            // Only Surname will be validated
            //var validationResult = validator.Validate(customer, options =>
            //{
            //    options.IncludeProperties(x => x.Surname);
            //});

            // Wildcard [] is used to indicate all items of a collection
            // The Total property of every Order will be validated
            //var validationResult = validator.Validate(customer, options =>
            //{
            //    options.IncludeProperties("Orders[].Total");
            //});
            #endregion

            #region ThrowExceptionsOnValidationErrors
            // The lines below are equivalent and require FluentValidation using

            //_validator.ValidateAndThrow(customer);

            //var validationResults = _validator.Validate(customer,
            //    options => options.ThrowOnFailures());
            #endregion

            #region IncludeRuleSets
            //var validationResults = validator.Validate(customer, options =>
            //{
            //    options.IncludeRuleSets("DefaultCascadeBehaviour");
            //});
            #endregion

            #region RootContextData

            //var context = new ValidationContext<Customer>(customer);
            //context.RootContextData["MyCustomData"] = "Test";
            //var validationResults = validator.Validate(context);

            #endregion

            var validationResults = validator.Validate(customer);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }
    }
}
