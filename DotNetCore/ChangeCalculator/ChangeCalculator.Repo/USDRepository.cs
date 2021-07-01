using ChangeCalculator.Core.Repositories;
using ChangeCalculator.ViewModels.Calculator;
using System.Collections.Generic;
using System.Globalization;

namespace ChangeCalculator.Repo
{
    public class USDRepository :Repository, IUSDRepository
    {
        public Calculator GetUSDDenominations(Calculator calculator)
        {
            int notes, paisa;
            decimal amount = calculator.PresentedAmount - calculator.ProductPrice;
            string s = amount.ToString("0.00", CultureInfo.InvariantCulture);
            string[] parts = s.Split('.');
            int rupeeAmount = int.Parse(parts[0]);
            int paisaAmount = int.Parse(parts[1]);
            Calculator calc = new Calculator();
            calc.TotalChange = calculator.PresentedAmount - calculator.ProductPrice;
            List<string> denominationsList = new List<string>();
            if (calculator.CurrencyType == "USD")
            {
                int[] rupeeDenominations = new int[] { 100, 50, 20, 10, 5, 2, 1 };
                int[] paiseDenominations = new int[] { 50, 25, 10, 5, 1 };

                for (int i = 0; i < 6; i++)
                {
                    notes = rupeeAmount / rupeeDenominations[i];

                    if (notes > 0)
                    {
                        rupeeAmount = rupeeAmount % rupeeDenominations[i]; // remaining money
                        denominationsList.Add(notes + " x $" + rupeeDenominations[i]);
                    }
                }

                for (int j = 0; j < 5; j++)
                {
                    paisa = paisaAmount / paiseDenominations[j];

                    if (paisa > 0)
                    {
                        paisaAmount = paisaAmount % paiseDenominations[j]; // remaining money
                        denominationsList.Add(paisa + " x " + paiseDenominations[j] + " ¢");
                    }
                }


                calc.Denominations = denominationsList;

            }

            return calc;
        }
    }
}
