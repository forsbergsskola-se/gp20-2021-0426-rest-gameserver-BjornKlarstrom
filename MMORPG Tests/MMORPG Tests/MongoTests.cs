using System;
using MMORPG.Models;
using MMORPG.Repositories;
using MongoDB.Bson.Serialization;
using NUnit.Framework;

namespace MMORPG_Tests{
    public class MongoTests{

        IRepository mongoDb;

        [SetUp]
        public void Setup(){
            this.mongoDb = new MongoDbRepository();
        }

        [Test]
        public void AddPlayerToDatabase(){
            
            var player = new Player{
                Name = "TestName",
                Level = 1,
                IsDeleted = false,
                Score = 0,
                CreationTime = DateTime.Now.ToUniversalTime()
            };

            try{
                var createNewPlayerTask = mongoDb.Create(player);
                Console.WriteLine(createNewPlayerTask.Result.Name);
                Assert.AreEqual(player.Name, createNewPlayerTask.Result.Name);
            }
            catch (Exception exception){
                Console.WriteLine(exception);
                Assert.Fail(exception.GetBaseException().Message);
            }
        }

        [Test]
        public void GetAllPLayersInDb(){
            try{
                var getAllPLayersTask = mongoDb.GetAll();
                Console.WriteLine($"Number of players in db is: {getAllPLayersTask.Result.Length}");
                foreach (var player in getAllPLayersTask.Result){
                    Console.WriteLine(player.Name);
                }
                
                Assert.AreEqual("TestName", getAllPLayersTask.Result[0].Name);
                Assert.Less(0, getAllPLayersTask.Result.Length);
            }
            catch (Exception exception){
                Console.WriteLine(exception.GetBaseException().Message);
                Assert.Fail(exception.GetBaseException().Message);
            }
        }

        [Test]
        public void GetPlayerFromDb(){
            try{
                var playerId = Guid.Parse("8806F38D-6FA1-4806-834B-E16B02FDE1F4");
                var getPlayerTask = mongoDb.Get(playerId);
                
                Console.WriteLine($"Name of player is: {getPlayerTask.Result.Name}");
                Assert.AreEqual("TestName", getPlayerTask.Result.Name);
            }
            catch (Exception exception){
                Console.WriteLine(exception);
                Assert.Fail(exception.GetBaseException().Message);
            }
        }
        
        [Test]
        public void GetPlayerAndModifyInDataBase(){
            try{
                var modifiedPlayer = new ModifiedPlayer{
                    Score = 100
                };
                var playerId = Guid.Parse("230D3A2D-354A-4723-91EF-737C6682FBE7");
                var getPlayerTask = mongoDb.Get(playerId);
                getPlayerTask.Wait();
                
                var getModifiedPlayersTask = mongoDb.Modify(getPlayerTask.Result.Id, modifiedPlayer);
                getModifiedPlayersTask.Wait();
                
                var resultScore = getPlayerTask.Result.Score + modifiedPlayer.Score;
                Console.WriteLine($"resultScore: {resultScore}, modifiedPlayer: {modifiedPlayer.Score}");
                Assert.AreEqual(resultScore, getModifiedPlayersTask.Result.Score);
            }
            catch (Exception exception){
                Console.WriteLine(exception.GetBaseException().Message);
                Assert.Fail(exception.GetBaseException().Message);
            }
        }
    }
}