using System;
using System.ComponentModel.DataAnnotations;
using MMORPG.Validation;
using MongoDB.Bson;

namespace MMORPG.Models{
    
    public class Item{
        
        public Guid Id { get; set; }
        [Range(0,99)] public int Level{ get; set; }
        
        [EnumDataType(typeof(ItemType))] public ItemType ItemType{ get; set; }
        
        [DateValidation] public DateTime CreationTime{ get; set; }

        public static Item CreateNewItem(NewItem newItem){
            var item = new Item{
                Id = Guid.NewGuid(),
                Level = 1,
                ItemType = newItem.ItemType,
                CreationTime = DateTime.UtcNow
            };
            return item;
        }
    }
}