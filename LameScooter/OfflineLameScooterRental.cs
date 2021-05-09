using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace LameScooter{
    public class OfflineLameScooterRental : ILameScooterRental{
        public Task<int> GetScooterCountInStation(string stationName){

            try{
                var scootersData = File.ReadAllText("scooters.json");
                var stationList = JsonConvert.DeserializeObject<List<Station>>(scootersData);

                foreach (var station in stationList!.Where(station => station.Name == stationName)){
                    return Task.FromResult(station.BikesAvailable);
                }

                throw new Exception($"{stationName} don't exist");
            }
            catch (Exception exception){
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}