using FluentValidationExamples.Enums;
using FluentValidationExamples.Models.Interfaces;

namespace FluentValidationExamples.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public int CustomerDiscount { get; set; }
        public int MaxCustomerDiscount { get; set; }
        public int MinCustomerDiscount { get; set; }
        public string Email { get; set; }
        public bool IsPreferredCustomer { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardName { get; set; }
        public string Photo { get; set; }
        public CustomerType CustomerType { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
