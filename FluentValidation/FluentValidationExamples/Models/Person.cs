using FluentValidationExamples.Models.Interfaces;

namespace FluentValidationExamples.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public IAddress Address { get; set; }
        public List<IAddress> Addresses { get; set; }
    }
}
