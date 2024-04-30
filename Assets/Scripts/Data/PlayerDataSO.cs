using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player/Player Data", order = 1)]
    public class PlayerDataSO : ScriptableObject
    {
        public const string PersistentDataName = "PlayerData";

        [SerializeField] private float movementVelocity = 5f;
        [SerializeField] private Vector3 defaultPosition;
        [SerializeField] private Vector3 lastPosition;
        [SerializeField] private PlayerInventory inventory;

        public float MovementVelocity => movementVelocity;
        public PlayerInventory Inventory => inventory;
        public Vector3 LastPosition
        {
            get => lastPosition;
            set => lastPosition = value;
        }

        public void Initialize()
        {
            lastPosition = defaultPosition;
            inventory.Initialize();
        }
    }
}