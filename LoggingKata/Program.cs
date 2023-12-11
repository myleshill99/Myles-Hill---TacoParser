using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using LoggingKata;
using System.ComponentModel.DataAnnotations;

namespace LoggingKata 
{ 
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // Objective: Find the two Taco Bells that are the farthest apart from one another.
            // Some of the TODO's are done for you to get you started. 

            logger.LogInfo("Log initialized");

            // Use File.ReadAllLines(path) to grab all the lines from your csv file. 
            // Optional: Log an error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            // This will display the first item in your lines array
            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Use the Select LINQ method to parse every line in lines collection
            var locations = lines.Select(parser.Parse).ToArray();
           
            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;
            double finalDistance = 0;
            double testDistance = 0;
            var geo1 = new GeoCoordinate();
            var geo2 = new GeoCoordinate();

            for (int i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                geo1.Latitude = locA.Location.Latitude;
                geo1.Longitude = locA.Location.Longitude;
                
                for (int j = 0; j < locations.Length; j++)
                {
                    var locB = locations[j];
                    geo2.Latitude = locB.Location.Latitude;
                    geo2.Longitude = locB.Location.Longitude;

                    testDistance = geo1.GetDistanceTo(geo2);

                    if (finalDistance <= testDistance)
                    {
                        finalDistance = testDistance;
                        tacoBell1 = locA;
                        tacoBell2 = locB;
                    }
                }
            }

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
            // Display these two Taco Bell locations to the console.
            Console.WriteLine($"The two furthest locations apart are {tacoBell1.Name} and {tacoBell2.Name} ({finalDistance}) km");
        }
    }
}
