using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidation;
using FluentValidationExamples.Validators.Customization;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // https://docs.fluentvalidation.net/en/latest/rulesets.html
    public class RuleSetsCustomerController : Controller
    {
        private IFactory _factory;
        public RuleSetsCustomerController(IFactory factory)
        {
            _factory = factory;
        }

        [HttpPost(Name = "RuleSetsCustomer")]
        public IActionResult CustomerPost(Customer customer)
        {
            var validator = _factory.Create<RuleSetCustomerValidator>();

            var validationResults = validator.Validate(customer, options =>
            {
                // If you call Validate without passing a ruleset then only rules not in a RuleSet will be executed.
                options.IncludeRuleSets("NamesRequired", "CustomerDiscount");
            });

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }

        [HttpPut(Name = "RuleSetsCustomer")]
        public IActionResult CustomerPut(Customer customer)
        {
            var validator = _factory.Create<RuleSetCustomerValidator>();

            var validationResults = validator.Validate(customer, options =>
            {
                // "default" is used for rules that aren't included in any of the Rule Sets
                // in CustomerRuleSetValidator it's RuleFor(x => x.Id).NotEqual(0);
                options.IncludeRuleSets("CustomerDiscount", "default");
                // You can also use the .IncludeRulesNotInRuleSet() method instead of passing "default"
            });

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }

        [HttpDelete(Name = "RuleSetsCustomer")]
        public IActionResult CustomerDelete(Customer customer)
        {
            var validator = _factory.Create<RuleSetCustomerValidator>();

            var validationResults = validator.Validate(customer, options =>
            {
                options.IncludeAllRuleSets();
            });

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }
    }
}
