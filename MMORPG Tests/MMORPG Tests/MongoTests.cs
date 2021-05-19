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
                Console.WriteLine(createNewPlayerTask.Result.Level);
                Assert.AreEqual(player.Name, createNewPlayerTask.Result.Name);
            }
            catch (Exception e){
                Console.WriteLine(e);
                Assert.Fail(e.GetBaseException().Message);
            }
        }
    }
}