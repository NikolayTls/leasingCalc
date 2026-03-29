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

        [HttpPost]
        public IActionResult CalculateCredit([FromBody] CreditCalculatorInput input)
        {
            decimal monthlyRate = (input.InterestRate / 100m) / 12m;
            int n = input.Months;

            decimal monthlyPayment =
                input.LoanAmount * monthlyRate /
                (1 - (decimal)Math.Pow((double)(1 + monthlyRate), -n));

            decimal totalPaid = monthlyPayment * n;
            decimal interest = totalPaid - input.LoanAmount;

            decimal years = n / 12m;
            decimal apr = (interest / input.LoanAmount) / years * 100m;

            var result = new CreditCalculatorResult
            {
                MonthlyPayment = Math.Round(monthlyPayment, 2),
                TotalPaid = Math.Round(totalPaid, 2),
                Interest = Math.Round(interest, 2),
                Apr = Math.Round(apr, 4)
            };

            return Json(result);
        }
    }
}
