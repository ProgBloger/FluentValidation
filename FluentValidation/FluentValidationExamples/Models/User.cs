using FluentValidationExamples.Enums;

namespace FluentValidationExamples.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Question QuestionId { get; set; }
        public string QuestionAnswer { get; set; }
        public int Credits { get; internal set; }
        public int Cash { get; internal set; }
    }
}
