using System;
using Microsoft.AspNetCore.Mvc;
using AspNetCore21App.Models;

namespace AspNetCore21App.Controllers
{
    public class HomeController : Controller

    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Calculates the resulting Total Paid, Total Fees, and Annual Percentage Rate (APR/ГПР)
        /// based on the user's provided input parameters.
        /// </summary>
        /// <param name="input">The requested calculator input (Price, Down Payment, Months, Installment, Processing Fee).</param>
        /// <returns>A JSON serialized object of type CreditCalculatorResult.</returns>
        [HttpPost]
        public IActionResult CalculateCredit([FromBody] CreditCalculatorInput input)
        {
            // Initial fee is calculated as a percentage of the financed amount.
            decimal financedAmount = input.ItemPrice - input.DownPayment;
            decimal totalFees = financedAmount * (input.ProcessingFeePercentage / 100m);
            
            // Total paid over the lifetime of the loan, including initial payments and fees.
            decimal totalPaid = input.DownPayment + (input.MonthlyInstallment * input.Months) + totalFees;

            // The net amount actually financed after deducting initial fees.
            decimal netLoanAmount = financedAmount - totalFees;
            
            // Derive the real internal monthly interest rate.
            double r = CalculateIRR((double)netLoanAmount, (double)input.MonthlyInstallment, input.Months);
            decimal apr = 0;
            if (r > 0)
            {
                // Convert monthly IRR (r) to annualized percentage rate (APR/ГПР).
                apr = (decimal)(Math.Pow(1 + r, 12) - 1) * 100m;
            }
            
            var result = new CreditCalculatorResult
            {
                Apr = Math.Round(apr, 2),
                TotalPaid = Math.Round(totalPaid, 2),
                TotalFees = Math.Round(totalFees, 2)
            };

            return Json(result);
        }

        /// <summary>
        /// Calculates the Internal Rate of Return (IRR) on a monthly basis
        /// utilizing the Secant numerical method (Root-finding algorithm).
        /// </summary>
        /// <param name="presentValue">The initial net loan amount (principal minus upfront fees).</param>
        /// <param name="pmt">The monthly installment amount.</param>
        /// <param name="n">The total number of months.</param>
        /// <returns>The calculated monthly rate (r).</returns>
        private double CalculateIRR(double presentValue, double pmt, int n)
        {
            if (pmt * n <= presentValue || presentValue <= 0) return 0;

            double r0 = 0.001; 
            double r1 = 0.01;  
            double f0 = NpvFunc(r0, presentValue, pmt, n);
            double f1 = NpvFunc(r1, presentValue, pmt, n);

            // Maximum iterations to prevent infinite loop.
            for (int i = 0; i < 50; i++)
            {
                if (Math.Abs(f1 - f0) < 1e-10) break;
                // Secant method approximation calculation.
                double r2 = r1 - f1 * (r1 - r0) / (f1 - f0);
                double f2 = NpvFunc(r2, presentValue, pmt, n);

                if (Math.Abs(f2) < 1e-7) return r2;

                r0 = r1;
                f0 = f1;
                r1 = r2;
                f1 = f2;
            }
            return r1;
        }

        /// <summary>
        /// Represents the Net Present Value function mapped to zero for finding the required return rate.
        /// Evaluates standard annuity logic: PMT * (1 - (1+r)^-n) / r - PV = 0.
        /// </summary>
        private double NpvFunc(double r, double pv, double pmt, int n)
        {
            return pmt * (1 - Math.Pow(1 + r, -n)) / r - pv;
        }
    }
}
