using KittyFarm.Data;
using KittyFarm.UI;
using UnityEngine;

namespace KittyFarm
{
    public class HandItem : MonoBehaviour
    {
        public ItemDataSO Current { get; private set; }
        public bool NotEmpty => Current is not null;

        private TalkBubble talkBubble;

        private void Awake()
        {
            talkBubble = GetComponent<TalkBubble>();
        }

        private void OnEnable()
        {
            GameView.SelectedItemChanged += OnSelectedItemChanged;
        }
        
        private void OnDisable()
        {
            GameView.SelectedItemChanged -= OnSelectedItemChanged;
        }

        public bool Is(ItemType type)
        {
            return Current is not null && Current.Type == type;
        }

        private void OnSelectedItemChanged(ItemDataSO itemData)
        {
            Current = itemData;

            if (Current != null)
            {
                talkBubble.Show($"这是{Current.ItemName}");
            }
        }
    }
}