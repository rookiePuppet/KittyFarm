using System.Linq;
using KittyFarm.Data;
using KittyFarm.MapClick;
using KittyFarm.Service;
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
                var basket = ServiceCenter.Get<IItemService>()
                    .TakeUsableItem(HandItem.Current, transform.position, Vector3Int.zero) as Basket;
                basket.CollectTarget = resource;
                var canUse = basket.TryUse(out var explanation);
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