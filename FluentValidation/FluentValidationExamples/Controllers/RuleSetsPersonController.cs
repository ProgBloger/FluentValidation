using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidation;
using FluentValidationExamples.Validators.Customization;
using FluentValidationExamples.Validators.SupportExamples;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // https://docs.fluentvalidation.net/en/latest/rulesets.html
    public class RuleSetsPersonController : Controller
    {
        private IFactory _factory;
        private IValidator<Person> _validator;
        public RuleSetsPersonController(IValidator<Person> validator)
        {
            _validator = validator;
        }

        [HttpPost(Name = "RuleSetsPerson")]
        public IActionResult PersonPost(Person person)
        {
            var validationResults = _validator.Validate(person, options =>
            {
                options.IncludeRuleSets("NamesRequired");
            });

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }
    }
}
