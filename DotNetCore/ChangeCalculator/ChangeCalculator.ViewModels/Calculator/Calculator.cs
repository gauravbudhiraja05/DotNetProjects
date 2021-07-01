using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChangeCalculator.ViewModels.Calculator
{
    public class Calculator
    {
        [Required(ErrorMessage = "Please select the Currency Type")]
        public string CurrencyType { get; set; }

        public string[] Currency = new[] { "GBP", "EUR", "USD" };


        [Required(ErrorMessage = "Please enter the Presented Amount")]
        public decimal PresentedAmount { get; set; }

        [Required(ErrorMessage = "Please enter the Product Price")]
        public decimal ProductPrice { get; set; }

        public decimal TotalChange { get; set; }

        public List<string> Denominations { get; set; }

    }
}
