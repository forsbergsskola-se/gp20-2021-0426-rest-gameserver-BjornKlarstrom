using System;
using System.Threading.Tasks;
using MMORPG.Models;

namespace MMORPG.Repositories{
    public interface IRepository{
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(NewPlayer player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);
    }
}