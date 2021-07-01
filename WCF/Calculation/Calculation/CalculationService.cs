using System.ServiceModel;

namespace Calculation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public  class CalculationService : ICalculationService
    {
        public double Add(double n1, double n2)
        {
            return n1 + n2;
        }
        public double Subtract(double n1, double n2)
        {
            return n1 - n2;
        }
        public double Multiply(double n1, double n2)
        {
            return n1 * n2;
        }       
        public double Divide(double n1, double n2)
        {
            return n1 / n2;
        }

    }
}
