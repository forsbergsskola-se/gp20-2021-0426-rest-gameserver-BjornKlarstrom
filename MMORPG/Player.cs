using System;

namespace MMORPG{
    public class Player{
        public Guid Id {get; set;}
        public string Name {get; set;}
        public int Score { get; set; }
        public int Level{ get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }

        public Player(string name){
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Score = 0;
            this.Level = 0;
            this.IsDeleted = false;
            this.CreationTime = DateTime.UnixEpoch;
        }

        public Player() : this(string.Empty){
            Id = Guid.Empty;
            CreationTime = DateTime.UnixEpoch;
        }
    }
}