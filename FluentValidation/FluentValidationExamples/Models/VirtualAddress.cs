using FluentValidationExamples.Models.Interfaces;

namespace FluentValidationExamples.Models
{
    public class VirtualAddress : IAddress
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public DateTime Expires { get; set; }
    }
}
