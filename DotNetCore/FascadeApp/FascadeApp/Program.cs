using System;

namespace FascadeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // The phone has been booted up and all the controllers are running
            GPSController gps = new GPSController();
            MobileDataController data = new MobileDataController();
            MusicController zune = new MusicController();
            WifiController wifi = new WifiController();

            ///////////// Going for Jogging /////////////////////

            // 1. Turn off the wifi
            wifi.IsSwitchedOn = false;

            // 2. Switch on the Mobile Data
            data.IsSwitchedOn = true;

            // 3. Turn on the GPS
            gps.IsSwitchedOn = true;

            // 4. Turn on the Music
            zune.IsSwitchedOn = true;

            // 5. Start the Sports-Tracker
            SportsTrackerApp app = new SportsTrackerApp();
            app.Start();

            ///////////// Back from Jogging /////////////////////
            Console.WriteLine();

            // 0. Share Sports tracker stats on twitter and facebook
            app.Share();

            // 1. Stop the Sports Tracker
            app.Stop();

            // 2. Turn off the Music
            zune.IsSwitchedOn = false;

            // 3. Turn off the GPS
            gps.IsSwitchedOn = false;

            // 4. Turn off the Mobile Data
            data.IsSwitchedOn = false;

            // 5. Turn on the wifi
            wifi.IsSwitchedOn = true;

            Console.ReadLine();
        }
    }
}
