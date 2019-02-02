using System;
using GeoLib.Services;
using System.ServiceModel;

namespace GeoLib.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostGeoManager = new ServiceHost(typeof(GeoManager));
            hostGeoManager.Open();

            Console.WriteLine("Services started. Pres [Enter] to exit.");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
