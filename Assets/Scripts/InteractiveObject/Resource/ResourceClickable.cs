using KittyFarm.Data;
using KittyFarm.MapClick;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.InteractiveObject
{
    public class ResourceClickable : MonoBehaviour, IPointerClickHandler
    {
        private Resource resource;

        private HandItem HandItem => GameManager.Player.HandItem;

        private void Awake()
        {
            resource = GetComponent<Resource>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (HandItem.Is(ItemType.Basket))
            {
                var usableItem = UsableItemSet.TakeUsableItem(HandItem.Current, transform.position, Vector3Int.zero);
                Basket.CollectTarget = resource;
                var canUse = usableItem.TryUse(out var explanation);
                if (!canUse)
                {
                    UIManager.Instance.ShowMessage(explanation);
                }
            }
            else
            {
                UIManager.Instance.ShowMessage("需要使用篮子收获");
            }
        }
    }
}