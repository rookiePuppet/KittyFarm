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
                ItemType.Seed => 0.5f,
                ItemType.FarmProduct => 2f,
                ItemType.Hoe => 1f,
                ItemType.WateringCan => 0.5f,
                _ => OperationRange
            };
        }
    }

    public enum ItemType
    {
        Seed,
        FarmProduct,
        Hoe,
        WateringCan
    }
}