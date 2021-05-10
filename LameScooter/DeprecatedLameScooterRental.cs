using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace LameScooter{
    public class DeprecatedLameScooterRental : ILameScooterRental{
        static string FilePath  => "scooters.txt";

        static async Task<Dictionary<string, int>> GetDictionaryTextFile(){

            var stationsText = await File.ReadAllTextAsync(FilePath);
            var splitText = stationsText.Split(':', StringSplitOptions.TrimEntries);

            Console.WriteLine(splitText[0]);
            
            return null;
        }
        
        public async Task<int> GetScooterCountInStation(string stationName){
            await GetDictionaryTextFile();
            return 55;
        }
    }
}