using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MMORPG.Repositories{
    public class FileRepository : IRepository{
        
        const string fileName = "game-dev.txt";
        static readonly string path = Path.Combine(Environment.CurrentDirectory, fileName);

        static async Task<List<Player>> ReadFileRepository(){
            try{
                using var streamReader = new StreamReader(path);
                var data = await streamReader.ReadToEndAsync();
                var result = JsonSerializer.Deserialize<List<Player>>(data);
                
                if(result == null) return new List<Player>();
                
                streamReader.Close();
                return result;
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw;
            }
        }
        
        
        public Task<Player> Get(Guid id){
            throw new NotImplementedException();
        }

        public async Task<Player[]> GetAll(){
            var players = await ReadFileRepository() ?? new List<Player>();
            return players.ToArray();
        }

        public async Task<Player> Create(NewPlayer newPlayer)
        {
            var players = await GetAll();
            var list = players.ToList();
            var addedPlayer = Player.CreateNewPlayer(newPlayer);
            list.Add(addedPlayer);
            var json = JsonConvert.SerializeObject(players);
            await File.WriteAllTextAsync(fileName, json);
            return addedPlayer;
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            throw new NotImplementedException();
        }

        public Task<Player> Delete(Guid id){
            throw new NotImplementedException();
        }
    }
}