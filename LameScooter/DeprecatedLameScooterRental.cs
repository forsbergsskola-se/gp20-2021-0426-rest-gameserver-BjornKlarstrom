using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace LameScooter{
    public class DeprecatedLameScooterRental : ILameScooterRental{
        static string FilePath  => "scooters.txt";

        static async Task<Dictionary<string, int>> GetDictionaryTextFile(){
            
            var stationsDictionary = new Dictionary<string,int>();

            var stationsText = await File.ReadAllTextAsync(FilePath);
            var splitText = stationsText.Split('\n', StringSplitOptions.TrimEntries);

            foreach (var split in splitText){
                var keyValue = split.Split(':', StringSplitOptions.TrimEntries);
                Console.WriteLine(keyValue[0]);
                stationsDictionary.Add(keyValue[0],int.Parse(keyValue[1]));
            }


            return null;
        }
        
        public async Task<int> GetScooterCountInStation(string stationName){
            await GetDictionaryTextFile();
            return 55;
        }
    }
}