using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MMORPG{
    
    
    [ApiController]
    [Route("players")]
    public class PlayersController : ControllerBase{
        
        readonly IRepository repository;
        public PlayersController(IRepository repository){
            this.repository = repository;
        }

        [HttpGet("Get")]
        public Task<Player> Get(Guid id){
            return repository.Get(id);
        }

        [HttpGet("GetAll")]
        public Task<Player[]> GetAll(){
            return repository.GetAll();
        }

        [HttpPost("Create")]
        public async Task<Player> Create(NewPlayer player){
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
