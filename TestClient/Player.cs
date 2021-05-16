using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestClient{
    public class Player{
        public Guid Id {get; set;}
        public string Name {get; set;}
        public int Score { get; set; }
        public int Level{ get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }

        public static async Task<Player[]> GetAllPlayers(){
            
            var url = new Uri($"{Program.urlRoot}/players");
            var getter = new RestGet(url);
            var responseData = await getter.Get();
            var players = JsonConvert.DeserializeObject<List<Player>>(responseData)?.ToArray();
            return players;
        }

        public static async Task<Player> CreatePlayer(string name){
            
            var url = new Uri($"{Program.urlRoot}/players");
            var poster = new RestPost(url, new NewPlayer(){Name = name});
            var responseData = await poster.Post();
            var createdPlayer = JsonConvert.DeserializeObject<Player>(responseData);
            return createdPlayer;
        }
    }
}