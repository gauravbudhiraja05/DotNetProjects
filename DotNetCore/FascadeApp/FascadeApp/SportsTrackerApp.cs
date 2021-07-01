using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FascadeApp
{
    class SportsTrackerApp
    {
        public void Start()
        {
            Console.WriteLine("Sports Tracker App STARTED");
        }

        public void Stop()
        {
            Console.WriteLine("Sports Tracker App STOPPED");
        }

        public void Share()
        {
            Console.WriteLine("Sports Tracker: Stats shared on twitter and facebook.");
        }
    }
}
