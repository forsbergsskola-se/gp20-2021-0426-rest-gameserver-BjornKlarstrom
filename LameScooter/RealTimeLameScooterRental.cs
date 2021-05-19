using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace LameScooter{
    public class RealTimeLameScooterRental : ILameScooterRental{
        readonly HttpClient httpClient = new HttpClient();
        public async Task<int> GetScooterCountInStation(string stationName){
            const string databaseUrl =
                "https://raw.githubusercontent.com/marczaku/"+
                "GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json";
            
            if (stationName.Any(char.IsDigit)) 
                throw new ArgumentException("Invalid Message (No numbers allowed in name)");

            var databaseFile = await httpClient.GetStringAsync(databaseUrl);
            var databaseObject = JsonConvert.DeserializeObject<LameScooterStationList>(databaseFile);

            if (databaseObject == null) throw new Exception("StationList is NULL");
            foreach (var station in databaseObject.Stations.Where(station => station.Name == stationName)){
                return station.BikesAvailable;
            }
            throw new NotFoundException($"Could not find station: {stationName}");
        }
    }
}