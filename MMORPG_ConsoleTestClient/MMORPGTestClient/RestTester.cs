using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MMORPGTestClient{
    
    public class RestTester{
        
        readonly HttpClient httpClient = new HttpClient();
        
        public async Task<Player[]> GetPlayers(string stationName){
            const string databaseUrl =
                "https://localhost:5001/api/players";
            
            if (stationName.Any(char.IsDigit)) 
                throw new ArgumentException("Invalid Message (No numbers allowed in name)");

            var databaseFile = await httpClient.GetStringAsync(databaseUrl);
            var players = JsonConvert.DeserializeObject<Player[]>(databaseFile);

            if (players == null) throw new Exception("Players is NULL");

            return players;
        }
    }
}