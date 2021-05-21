using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Models;
using MMORPG.MongoExceptions;
using MMORPG.Repositories;
using Newtonsoft.Json;

namespace MMORPG.Controllers{
    
    [ApiController]
    [Route("players")]
    public class PlayersController : ControllerBase{
        
        readonly IRepository repository;
        public PlayersController(IRepository repository){
            this.repository = repository;
        }
        
        [HttpGet("All")]
        public async Task<IActionResult> GetPlayers(){
            try{
                var players = await repository.GetAll();
                var jsonPlayers = JsonConvert.SerializeObject(players);
                return Ok(jsonPlayers);
            }
            catch (Exception e){
                return e.GetBaseException().GetHttpCodeStatus();
            }
        }

        [HttpGet("Get")]
        public Task<Player> Get(Guid id){
            return repository.Get(id);
        }

        [HttpPost("Create")]
        public async Task<Player> Create(Player player){
            return await repository.Create(player);
        }

        [HttpPut("Modify")]
        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            return repository.Modify(id, player);
        }

        [HttpDelete("Delete")]
        public Task<Player> Delete(Guid id){
            return repository.Delete(id);
        }
    }
}
