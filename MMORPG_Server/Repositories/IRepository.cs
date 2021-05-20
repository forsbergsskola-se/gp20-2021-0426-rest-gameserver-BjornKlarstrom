using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Models;

namespace MMORPG.Repositories{
    public interface IRepository{
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);

        Task<Player> AddItem(Guid id, NewItem newItem);
        Task<List<Item>> GetAllItems(Guid id);
        Task ModifyItem(Guid id, Guid targetId, ModifiedItem modifiedItem);
        Task DeleteItem(Guid id, Guid deleteTargetId);
    }
}