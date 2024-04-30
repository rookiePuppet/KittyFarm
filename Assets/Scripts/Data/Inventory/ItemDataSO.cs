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
        public float OperationRange;

        protected virtual void OnValidate()
        {
            OperationRange = Type switch
            {
                ItemType.Seed => 1f,
                ItemType.FarmProduct => 2f,
                ItemType.Hoe => 1f,
                _ => OperationRange
            };
        }
    }

    public enum ItemType
    {
        Seed,
        FarmProduct,
        Hoe
    }
}