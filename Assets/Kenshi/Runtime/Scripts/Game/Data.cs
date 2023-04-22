namespace Kenshi
{
    public class Data
    {    
        public enum ItemType
        {
            General,
            Equipment
        }

        public enum WeaponType
        {
            Katana,
            Sword,
            Bow
        }

        public enum ArmorType
        {
            Cloth,
            Skin,
            Metal,
            SkinnedMetal,
            ChainMetal
        }

        public enum CharacterRace
        {
            Human,
            Goblin,
            Elf
        }
        
        public enum ItemCategory
        {
            Weapon,
            Armor,
            Cloth,
            Pants,
            HeadGear,
            Belt,
            Shoose
        }
        public class Item
        {
            public string Id { get; set; }
            public ItemCategory Category { get; set; }
            public ItemType ItemType { get; set; } 
        } 
        
    }
}