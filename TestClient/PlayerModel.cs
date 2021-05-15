using System;

namespace TestClient{
    public class PlayerModel{
        public Guid Id {get; set;}
        public string Name {get; set;}
        public int Score { get; set; }
        public int Level{ get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}