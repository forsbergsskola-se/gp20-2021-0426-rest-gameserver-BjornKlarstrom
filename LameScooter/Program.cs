using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter{

    class Program{
        static async Task Main(string[] args){

            ILameScooterRental scooterRental = new OfflineLameScooterRental();

            var scooterCount = scooterRental.GetScooterCountInStation(null); // Put command line arguments here
            Console.WriteLine($"Number of scooter available at this station is: {scooterCount}");
        }
    }
    
    public interface ILameScooterRental{
        Task<int> GetScooterCountInStation(string stationName);    
    }
    
    public class Stations{
        public List<ScooterStation> StationList{ get; set; }
    }

    public class ScooterStation{
        public string Id               { get; set; }
        public string Name             { get; set; }
        public float  X                { get; set; }
        public float  Y                { get; set; }
        public int    BikesAvailable   { get; set; }
        public int    SpacesAvailable  { get; set; }
        public int    Capacity         { get; set; }
        public bool   AllowDropOff     { get; set; }
        public bool   AllowOverloading { get; set; }
        public bool   IsFloatingBike   { get; set; }
        public bool   IsCarStation     { get; set; }
        public string State            { get; set; }
    }
}