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
        static readonly string repositoryPath = Path.Combine(Environment.CurrentDirectory, fileName);

        public async Task<Player> Get(Guid id){
            var players = await GetAll();
            return players.FirstOrDefault(player => player.Id == id);
        }

        public async Task<Player[]> GetAll(){
            var repoData = await File.ReadAllTextAsync(repositoryPath);
            var players = JsonConvert.DeserializeObject<List<Player>>(repoData);
            return players?.ToArray();
        }

        public async Task<Player> Create(Player newPlayer)
        {
            var players = await GetAll();
            var playerList = players.ToList();
            var addedPlayer = Player.CreateNewPlayer(new NewPlayer());
            playerList.Add(addedPlayer);
            var json = JsonConvert.SerializeObject(players);
            await File.WriteAllTextAsync(fileName, json);
            return addedPlayer;
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer){
            var players = await GetAll();

            foreach (var player in players){
                if (player.Id != id)
                    continue;

                player.Score = modifiedPlayer.Score;
                var newRepoData = JsonConvert.SerializeObject(players);

                await File.WriteAllTextAsync(repositoryPath, newRepoData);
                return player;
            }
            return null;
        }

        public async Task<Player> Delete(Guid id){
            var players = await GetAll();

            foreach (var player in players){
                if (player.Id != id)
                    continue;

                player.IsDeleted = true;
                var newRepoData = JsonConvert.SerializeObject(players);

                await File.WriteAllTextAsync(repositoryPath, newRepoData);
                return player;
            }
            return null;
        }

        public async Task<Player> AddItem(Guid id, NewItem newItem){
            var players = await GetAll();

            foreach (var player in players){
                if(player.Id != id)
                    continue;
                
                player.Items.Add(Item.CreateNewItem(newItem));
            }
            return null;
        }

        public async Task<List<Item>> GetAllItems(Guid id){
            var player = await Get(id);
            return player.Items;
        }

        public async Task ModifyItem(Guid id, Guid targetId, ModifiedItem modifiedItem){
            var player = await Get(id);

            foreach (var item in player.Items.Where(item => item.Id == targetId)){
                item.Level = modifiedItem.Level;
            }
        }

        public async Task DeleteItem(Guid id, Guid deleteTargetId){
            var players = await GetAll();

            foreach (var player in players.Where(item => item.Id == id)){
                player.Items.RemoveAll(item => item.Id == deleteTargetId);
            }
            
            var newRepoData = JsonConvert.SerializeObject(players);
            await File.WriteAllTextAsync(repositoryPath, newRepoData);
        }
    }
}