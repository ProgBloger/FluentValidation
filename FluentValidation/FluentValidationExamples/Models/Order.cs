namespace FluentValidationExamples.Models
{
    public class Order
    {
        public List<string> Tags { get; set; } = new List<string>();
        public int Total { get; set; }
        public int? Sum { get; set; }
    }
}
