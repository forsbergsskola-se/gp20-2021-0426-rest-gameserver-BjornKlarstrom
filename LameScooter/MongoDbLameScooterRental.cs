using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using static System.Threading.Tasks.Task<int>;

namespace LameScooter{
    public class MongoDbLameScooterRental : ILameScooterRental{
    
        readonly MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
        public Task<int> GetScooterCountInStation(string stationName){
            
            if (stationName.Any(char.IsDigit)) 
                throw new ArgumentException("Invalid Message (No numbers allowed in name)");

            Station station;
            
            var database = mongoClient.GetDatabase("LameScooterRentalDatabase");
            var collection = database.GetCollection<BsonDocument>("lamescooters");

            var filter = Builders<BsonDocument>.Filter.Eq("name", stationName);
            
            try{
                var stationDocument = collection.Find(filter).First();
                station = BsonSerializer.Deserialize<Station>(stationDocument);
            }
            catch{
                throw new NotFoundException($"Could not find station: {stationName}");
            }

            return Task.FromResult(station.BikesAvailable);
        }
    }
}