using System;
using MMORPG.Models;
using MMORPG.Repositories;
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
    }
}