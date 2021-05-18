using System;
using System.Threading.Tasks;
using MMORPG.Models;
using MMORPG.MongoExceptions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG.Repositories{
    
    public class MongoDbRepository : IRepository{

        const string databaseName = "game";
        const string localMongoUrl = "mongodb://localhost:27017";
        const string mongoCollectionName = "players";

        readonly IMongoDatabase mongoDatabase;

        public MongoDbRepository(){
            var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(localMongoUrl));
            var mongoClient = new MongoClient(mongoClientSettings);

            mongoDatabase = mongoClient.GetDatabase(databaseName);
        }
        
        public Task<Player> Get(Guid id){
            throw new NotImplementedException();
        }

        public async Task<Player[]> GetAll(){

            try{
                var collection = mongoDatabase.GetCollection<Player>(mongoCollectionName);
                var players = collection.Find(new BsonDocument()).ToListAsync();    
                
                if(await Task.WhenAny(players, Task.Delay(2000)) != players)
                    throw new RequestTimeOutException("408: Request Time Out");

                if (players.Result == null)
                    throw new NotFoundException("404: Not Found");

                return players.Result.ToArray();
            }
            catch (Exception exception){
                Console.WriteLine(exception);
                throw;
            }
        }

        public async Task<Player> Create(NewPlayer player){
            try{
                var collection = mongoDatabase.GetCollection<NewPlayer>(mongoCollectionName);
                var createdPlayer = collection.InsertOneAsync(player);
                if (await Task.WhenAny(createdPlayer, Task.Delay(2000)) == createdPlayer){
                    return Player.CreateNewPlayer(player);
                }
                throw new RequestTimeOutException("408: Request Time Out");
            }
            catch (Exception exception){
                Console.WriteLine(exception);
                throw;
            }
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            throw new NotImplementedException();
        }

        public Task<Player> Delete(Guid id){
            throw new NotImplementedException();
        }
    }
}