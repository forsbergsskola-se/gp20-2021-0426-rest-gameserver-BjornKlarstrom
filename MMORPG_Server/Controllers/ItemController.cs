using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Models;
using MMORPG.MongoExceptions;
using MMORPG.Repositories;

namespace MMORPG.Controllers{
    
    [ApiController]
    [Route("players/{playerId}/items")]
    public class ItemController : ControllerBase{

        readonly IRepository repository;
        public ItemController(IRepository repository){
            this.repository = repository;
        }
        
        [HttpPost("New")]
        public async Task<Player> NewItem(Guid playerId, NewItem newItem){
            var player = await repository.AddItem(playerId, newItem);
            if (player == null)
                throw new NotFoundException($"{playerId} Not Found");
            return player;
        }
        
        [HttpGet("All")]
        public async Task<List<Item>> GetAll(Guid playerId){
            var items = await repository.GetAllItems(playerId);
            return items;
        }
        
        [HttpPut("Modify")]
        public async Task Modify(Guid playerId, Guid originalItem, ModifiedItem modifiedItem){
            await repository.ModifyItem(playerId, originalItem, modifiedItem);
        }
        
        [HttpPost("Delete")]
        public async Task Delete(Guid playerId, Guid item){
            await repository.DeleteItem(playerId, item);
        }
    }
}