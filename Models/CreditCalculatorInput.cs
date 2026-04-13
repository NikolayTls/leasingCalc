namespace AspNetCore21App.Models
{
    public class CreditCalculatorInput
    {
        public decimal ItemPrice { get; set; }
        public decimal DownPayment { get; set; }
        public int Months { get; set; }
        public decimal MonthlyInstallment { get; set; }
        public decimal ProcessingFeePercentage { get; set; }
    }
}
