using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Models{
    
    [Serializable]
    public class Player{
        
        public static Player CreateNewPlayer(NewPlayer newPlayer)
        {
            var player = new Player
            {
                Id = Guid.NewGuid(),
                Name = newPlayer.Name,
                CreationTime = DateTime.UtcNow
            };
            return player;
        }
        
        [BsonId] public Guid Id {get; set;}
        public string Name {get; set;}
        [BsonElement("Score")] public int Score { get; set; }
        public int Level{ get; set; }
        [BsonElement("IsDeleted")] public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}