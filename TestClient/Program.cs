using System;
using System.Threading.Tasks;

namespace TestClient{
    class Program{
        
        public const string urlRoot = "https://localhost:5001";
        static async Task Main(string[] args){

            var currentPlayers = await Player.GetAllPlayers();

            var createdPlayer = await Player.CreatePlayer("Oskar");

            Console.WriteLine(createdPlayer);

            foreach (var player in currentPlayers){
                Console.WriteLine(player.Name);
            }
        }
    }
}