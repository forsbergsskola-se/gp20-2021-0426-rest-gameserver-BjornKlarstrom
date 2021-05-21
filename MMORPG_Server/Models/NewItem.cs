using System.ComponentModel.DataAnnotations;

namespace MMORPG.Models{
    public class NewItem{
        
        [EnumDataType(typeof(ItemType))]
        public ItemType ItemType{ get; set; }
    }
}