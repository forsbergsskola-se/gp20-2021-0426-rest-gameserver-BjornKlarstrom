using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace LameScooter{
    public class OfflineLameScooterRental : ILameScooterRental{
        static string FilePath  => "scooters.json";
        public async Task<int> GetScooterCountInStation(string stationName){
            
            if (stationName.Any(char.IsDigit)) 
                throw new ArgumentException("Invalid Message (No numbers allowed in name)");

            var scootersData = File.ReadAllTextAsync(FilePath);
            var stationList = JsonConvert.DeserializeObject<List<Station>>(await scootersData);
            
            foreach (var station in stationList!.Where(station => station.Name == stationName)){
                return station.BikesAvailable;
            }
            throw new NotFoundException($"Could not find station: {stationName}");
        }
    }
}
                
