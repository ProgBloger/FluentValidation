using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerRuleSetController : Controller
    {
        private IValidator<Customer> _validator;

        public CustomerRuleSetController(IValidator<Customer> validator)
        {
            _validator = validator;
        }

        [HttpPost(Name = "PostCustomer")]
        public IActionResult PostCustomer(Customer customer)
        {
            //var validationResults = _validator.Validate(customer);
            var validationResults = _validator.Validate(customer, options =>
            {
                options.IncludeRuleSets("NamesRequired");
                //options.IncludeRuleSets("NamesRequired", "NotElonMusk");

                // The following two lines are equivalent
                //options.IncludeRuleSets("NamesRequired").IncludeRulesNotInRuleSet();
                //options.IncludeRuleSets("NamesRequired", "default");

                //options.IncludeAllRuleSets();
            });

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }

        [HttpPut(Name = "PutCustomer")]
        public IActionResult PutCustomer(Customer customer)
        {
            return null;
        }
    }
}
