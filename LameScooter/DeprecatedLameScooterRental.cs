using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace LameScooter{
    public class DeprecatedLameScooterRental : ILameScooterRental{
        static string FilePath  => "scooters.txt";

        public async Task<int> GetScooterCountInStation(string stationName){
            
            if (stationName.Any(char.IsDigit)) 
                throw new ArgumentException("Invalid Message (No numbers allowed in name)");
            
            var stationList = new List<Station>();

            var stationsText = await File.ReadAllTextAsync(FilePath);
            var splitText = stationsText.Split('\n', StringSplitOptions.TrimEntries);

            for(var i = 0; i < splitText.Length - 1; i++){
                var keyValue = splitText[i].Split(':', StringSplitOptions.TrimEntries);
                stationList.Add(new Station{
                    Name = keyValue[0],
                    BikesAvailable = int.Parse(keyValue[1])
                });
            }
            
            foreach (var station in stationList!.Where(station => station.Name == stationName)){
                return station.BikesAvailable;
            }
            throw new NotFoundException($"Could not find station: {stationName}");
        }
    }
}