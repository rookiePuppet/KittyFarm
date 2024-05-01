using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "NewItemData", menuName = "Data/Item Data")]
    public class ItemDataSO : ScriptableObject
    {
        public int Id;
        public string ItemName;
        public string Description;
        public Sprite IconSprite;
        public int Value;
        public float SoldDiscount;
        public ItemType Type;
    }

    public enum ItemType
    {
        Seed,
        FarmProduct,
        Hoe,
        HarvestTool
    }
}