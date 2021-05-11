using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LameScooter{
    public class RealTimeLameScooterRental : ILameScooterRental{
        readonly HttpClient httpClient = new HttpClient();
        public async Task<int> GetScooterCountInStation(string stationName){
            const string databaseUrl =
                "https://raw.githubusercontent.com/marczaku/"+
                "GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json";
            
            if (stationName.Any(char.IsDigit)) 
                throw new ArgumentException("Invalid Message (No numbers allowed in name)");

            var stationsText = await httpClient.GetStringAsync(databaseUrl);
            Console.WriteLine(stationsText);
            return 0;
        }
    }
}