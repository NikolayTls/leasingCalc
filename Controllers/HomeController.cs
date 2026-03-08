using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LeasingCalculator.Models;

namespace LeasingCalculator.Controllers
{
    /// <summary>
    /// The main controller responsible for serving the SPA-like interface and handling calculation requests.
    /// Built entirely to support the Leasing Calculator functional requirement logic.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Serves the main application point (index.cshtml) which holds the HTML and JS logic.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Endpoint that performs leasing calculations based on user input. 
        /// Returns a JSON object consumed by the frontend via AJAX to present calculation results dynamically.
        /// </summary>
        /// <param name="request">The parameters for calculations sent from the browser.</param>
        /// <returns>JSON result populated with calculated fees, installment rate, etc.</returns>
        [HttpPost]
        public IActionResult CalculateLease([FromBody] LeaseCalculationRequest request)
        {
            // Simple server side validation acting as a fallback for the UI validation logic
            if (request == null || request.Price <= 0 || request.Months <= 0)
            {
                return BadRequest("Invalid request parameters.");
            }

            // Calculation algorithm tailored for 'processing fee' parameter specifications
            decimal financedAmount = request.Price - request.DownPayment;
            if (financedAmount < 0) financedAmount = 0;

            decimal totalFees = financedAmount * (request.ProcessingFeePercent / 100m);
            decimal totalPaid = financedAmount + totalFees;
            decimal monthlyInstallment = totalPaid / request.Months;

            var result = new LeaseCalculationResult
            {
                FinancedAmount = financedAmount,
                TotalFees = totalFees,
                TotalPaid = totalPaid,
                MonthlyInstallment = monthlyInstallment
            };

            return Json(result);
        }
    }
}
