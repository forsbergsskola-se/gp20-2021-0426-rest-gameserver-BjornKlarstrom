using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LameScooter{
    public class OfflineLameScooterRental : ILameScooterRental{
        static string FilePath  => "scooters.json";
        public async Task<int> GetScooterCountInStation(string stationName){
            
            if (stationName.Any(char.IsDigit)) 
                throw new ArgumentException("Invalid Message (No numbers allowed in name)");

            var databaseFile = File.ReadAllTextAsync(FilePath);
            var databaseObject = JsonConvert.DeserializeObject<LameScooterStationList>(await databaseFile);

            if (databaseObject == null) throw new Exception("StationList is NULL");
            foreach (var station in databaseObject.Stations.Where(station => station.Name == stationName)){
                return station.BikesAvailable;
            }
            throw new NotFoundException($"Could not find station: {stationName}");
        }
    }
}
                
