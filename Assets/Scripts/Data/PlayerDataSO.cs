using System;
using KittyFarm.UI;
using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player/Player Data", order = 1)]
    public class PlayerDataSO : ScriptableObject
    {
        public static event Action<int> CoinsUpdated;

        public const string PersistentDataName = "PlayerData";

        [Header("Config")]
        [SerializeField] private float movementVelocity = 5f;
        [SerializeField] private Vector3 defaultPosition;

        [Header("Persistence Data")]
        [SerializeField] private int coins;
        [SerializeField] private Vector3 lastPosition;
        [SerializeField] private PlayerInventory inventory;

        public float MovementVelocity => movementVelocity;
        public PlayerInventory Inventory => inventory;
        public Vector3 LastPosition
        {
            get => lastPosition;
            set => lastPosition = value;
        }

        public int Coins => coins;

        public void Initialize()
        {
            lastPosition = defaultPosition;
            inventory.Initialize();
        }

        public void OnPurchasedCommodity(ItemDataSO itemData, int amount)
        {
            var totalValue = itemData.Value * amount;
            coins -= totalValue;

            CoinsUpdated?.Invoke(coins);

            inventory.AddItem(itemData, amount);
            
            UIManager.Instance.ShowMessage($"购买{itemData.ItemName}X{amount}，花费{totalValue}金币");
            UIManager.Instance.ShowUI<GetItemView>().Initialize(itemData, amount);
        }

        public void OnSoldItem(ItemDataSO itemData, int amount)
        {
            var income = Mathf.RoundToInt(itemData.Value * itemData.SoldDiscount * amount);

            coins += income;
            CoinsUpdated?.Invoke(coins);
            
            inventory.RemoveItem(itemData, amount);
            
            UIManager.Instance.ShowMessage($"卖出{itemData.ItemName}X{amount}，收入{income}金币");
        }
    }
}