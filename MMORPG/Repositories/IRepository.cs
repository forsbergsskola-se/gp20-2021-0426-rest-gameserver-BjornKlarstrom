using System;
using System.Threading.Tasks;
using MMORPG.Models;

namespace MMORPG.Repositories{
    public interface IRepository{
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);

        Task<Player> AddItem(Guid id);
        Task<Player> GetItem(Guid id);
        Task<Player> ModifyItem(Guid id, ModifiedItem modifiedItem);
        Task DeleteItem(Guid id);
    }
}