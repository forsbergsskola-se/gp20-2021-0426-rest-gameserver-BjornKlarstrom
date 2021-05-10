using System.Threading.Tasks;

namespace LameScooter{
    public class DeprecatedLameScooterRental : ILameScooterRental{
        public Task<int> GetScooterCountInStation(string stationName){
            throw new System.NotImplementedException();
        }
    }
}