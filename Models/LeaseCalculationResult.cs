namespace LeasingCalculator.Models
{
    /// <summary>
    /// Represents the calculated results returned back to the client as JSON.
    /// </summary>
    public class LeaseCalculationResult
    {
        public decimal FinancedAmount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalFees { get; set; }
        public decimal MonthlyInstallment { get; set; }
    }
}
