using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;
using FluentValidationExamples.Validators;
using FluentValidation;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        PersonValidator _personValidator;
        CustomerCascadeValidator _personCascadeValidator;

        public PersonController(PersonValidator personValidator, CustomerCascadeValidator personCascadeValidator)
        {
            _personValidator = personValidator;
            _personCascadeValidator = personCascadeValidator;
        }

        [HttpPost(Name = "PostPerson")]
        public IActionResult PostPerson(Person person)
        {
            var validationResults = _personValidator.Validate(person, options =>
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

        [HttpPut(Name = "PutPerson")]
        public IActionResult PutPerson(Person person)
        {
            return null;
        }
    }
}
