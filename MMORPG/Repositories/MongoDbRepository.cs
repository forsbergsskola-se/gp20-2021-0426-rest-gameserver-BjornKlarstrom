﻿using System;
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
        
        public async Task<Player> Get(Guid id){
            try{
                var collection = mongoDatabase.GetCollection<Player>(mongoCollectionName);
                var getPlayerTask = collection.FindAsync(player => player.Id == id);
                
                if (await Task.WhenAny(getPlayerTask, Task.Delay(2000)) != getPlayerTask)
                    throw new RequestTimeOutException("408: Request Time Out");
                
                if (getPlayerTask.Result == null)
                    throw new NotFoundException("404: Not Found");
                return getPlayerTask.Result.First();
            }
            catch (Exception exception){
                Console.WriteLine(exception);
                throw;
            }
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

        public async Task<Player> Create(Player player){
            try{
                var collection = mongoDatabase.GetCollection<Player>(mongoCollectionName);
                var createdPlayer = collection.InsertOneAsync(player);
                if (await Task.WhenAny(createdPlayer, Task.Delay(2000)) == createdPlayer){
                    return player;
                }
                throw new RequestTimeOutException("408: Request Time Out");
            }
            catch (Exception exception){
                Console.WriteLine(exception);
                throw;
            }
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer){
            try{
                var collection = mongoDatabase.GetCollection<Player>(mongoCollectionName);
                var playerToModify = 
                    Builders<Player>.Update.Inc("Score", modifiedPlayer.Score);
                var modifyTask = collection.FindOneAndUpdateAsync(player => player.Id == id, playerToModify);
                
                if (await Task.WhenAny(modifyTask, Task.Delay(2000)) != modifyTask)
                    throw new RequestTimeOutException("408: Request Time Out");
                if(modifyTask.Result == null)
                    throw new NotFoundException($"404: {nameof(Player)} not found");
                modifyTask.Result.Score += modifiedPlayer.Score;
                return modifyTask.Result;
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Task<Player> Delete(Guid id){
            throw new NotImplementedException();
        }
    }
}