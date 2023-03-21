using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using FluentValidationExamples.Models;

namespace FluentValidationExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IValidator<User> _validator;

        public UserController(IValidator<User> validator)
        {
            _validator = validator;
        }

        [HttpPost(Name = "PostUser")]
        public IActionResult PostUser(User user)
        {
            var validationResults = _validator.Validate(user);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            return Ok();
        }
    }
}
