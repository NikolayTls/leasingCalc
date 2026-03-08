namespace LeasingCalculator.Models
{
    /// <summary>
    /// Represents the payload sent from the client (browser) to request a lease calculation.
    /// </summary>
    public class LeaseCalculationRequest
    {
        public decimal Price { get; set; }
        public decimal DownPayment { get; set; }
        public int Months { get; set; }
        public decimal ProcessingFeePercent { get; set; } 
    }
}
