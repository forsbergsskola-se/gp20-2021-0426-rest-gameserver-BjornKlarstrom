using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LameScooter{
    public class DeprecatedLameScooterRental : ILameScooterRental{
        static string FilePath  => "scooters.txt";

        public async Task<int> GetScooterCountInStation(string stationName){
            
            if (stationName.Any(char.IsDigit)) 
                throw new ArgumentException("Invalid Message (No numbers allowed in name)");
            
            var stationList = new List<Station>();

            var databaseFile = await File.ReadAllTextAsync(FilePath);
            var lineSplitText = databaseFile.Split('\n', StringSplitOptions.TrimEntries);

            for(var i = 0; i < lineSplitText.Length - 1; i++){
                var keyValueText = lineSplitText[i].Split(':', StringSplitOptions.TrimEntries);
                stationList.Add(new Station{
                    Name = keyValueText[0],
                    BikesAvailable = int.Parse(keyValueText[1])
                });
            }
            
            foreach (var station in stationList!.Where(station => station.Name == stationName)){
                return station.BikesAvailable;
            }
            throw new NotFoundException($"Could not find station: {stationName}");
        }
    }
}