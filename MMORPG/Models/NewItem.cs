using System;
using System.ComponentModel.DataAnnotations;
using MMORPG.Validation;

namespace MMORPG.Models{
    public class NewItem{
        
        [EnumDataType(typeof(ItemType))]
        public ItemType ItemType{ get; set; }
    }
}