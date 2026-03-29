
namespace AspNetCore21App.Models
    { 
    public class CreditCalculatorInput
    {
        public decimal LoanAmount { get; set; }

        public int Months { get; set; }

        public decimal InterestRate { get; set; }
    }
}
