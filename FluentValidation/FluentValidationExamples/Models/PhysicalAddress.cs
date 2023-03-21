using FluentValidationExamples.Models.Interfaces;

namespace FluentValidationExamples.Models
{
    public class PhysicalAddress : IAddress
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
    }
}
