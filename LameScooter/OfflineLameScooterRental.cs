using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace LameScooter{
    public class OfflineLameScooterRental : ILameScooterRental{
        public async Task<int> GetScooterCountInStation(string stationName){
            var scooterData = await File.ReadAllTextAsync("scooters.json");
            var stationArray = JsonConvert.DeserializeObject<StationInfo[]>(scooterData);

            foreach (var stationInfo in stationArray){
                Console.WriteLine(stationInfo.Name);
            }

            var station = stationArray!.FirstOrDefault(info => info.Name == stationName);
            if (station == null)
                throw new NotFoundException(stationName);
            return station.BikesAvailable;
        }
    }

    public class NotFoundException : Exception{
        public NotFoundException(string stationName){
            Console.WriteLine($"{stationName} not found");
        }
    }
}