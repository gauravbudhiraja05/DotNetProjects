using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FascadeApp
{
    class MobileDataController
    {
        bool isSwitchedOn = false;

        public bool IsSwitchedOn
        {
            get
            {
                return isSwitchedOn;
            }
            set
            {
                isSwitchedOn = value;
                DisplayStatus();
            }
        }

        private void DisplayStatus()
        {
            string status = (isSwitchedOn == true) ? "ON" : "OFF";
            Console.WriteLine("Mobile Data Switched {0}", status);
        }
    }
}
