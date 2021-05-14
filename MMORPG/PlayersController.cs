using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MMORPG{
    public class PlayersController{

        IRepository repository;
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
        public Task<Player> Create(NewPlayer player){
            
            var newPlayer = new Player{
                Name = player.Name,
                CreationTime = DateTime.Now,
                Id = Guid.NewGuid()
            };
            return repository.Create(newPlayer);
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
