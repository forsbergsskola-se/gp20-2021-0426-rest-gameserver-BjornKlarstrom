using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LameScooter{
    public class OfflineLameScooterRental : ILameScooterRental{

        async Task<Stations> ScooterDataToList(){
            return JsonConvert.DeserializeObject<Stations>(await File.ReadAllTextAsync("scooter.json"));
        }
        public Task<int> GetScooterCountInStation(string stationName){
            throw new System.NotImplementedException();
        }
    }
}