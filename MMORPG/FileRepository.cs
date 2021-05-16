using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MMORPG{
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

        public async Task<Player> Create(NewPlayer newPlayer){
            
            var players = await ReadFileRepository() ?? new List<Player>();
            var player = new Player(newPlayer.Name);
            players.Add(player);

            await using var fileStream = File.OpenWrite(path);
            await JsonSerializer.SerializeAsync(fileStream, players);
            
            fileStream.Close();
            return player;
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            throw new NotImplementedException();
        }

        public Task<Player> Delete(Guid id){
            throw new NotImplementedException();
        }
    }
}