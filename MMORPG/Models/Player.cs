using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Models{
    
    [Serializable]
    public class Player{
        [BsonId] public Guid Id {get; set;}
        public string Name {get; set;}
        [BsonElement("Score")] public int Score { get; set; }
        
        public int Level{ get; set; }
        [BsonElement("IsDeleted")] public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        
        public List<Item> Items{ get; set; }
        
        public static Player CreateNewPlayer(NewPlayer newPlayer)
        {
            var player = new Player
            {
                Id = Guid.NewGuid(),
                Name = newPlayer.Name,
                CreationTime = DateTime.UtcNow,
                Items = new List<Item>()
            };
            return player;
        }
    }
}