using System.Threading.Tasks;

namespace LameScooter{
    public class Station : IStation{
        public int Id { get; set; } 
        public string Name { get; set; } 
        public float X { get; set; } 
        public float Y { get; set; } 
        public int BikesAvailable { get; set; } 
        public int SpacesAvailable { get; set; } 
        public int Capacity { get; set; } 
        public bool AllowDropOff { get; set; } 
        public bool  AllowOverloading { get; set; } 
        public bool IsFloatingBike { get; set; } 
        public bool IsCarStation { get; set; } 
        public string State { get; set; }
        public string[] Networks { get; set; }
        public bool RealTimeData{ get; set;}
    }
}