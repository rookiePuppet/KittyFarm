using System.Linq;
using KittyFarm.Service;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.Harvestable
{
    public class ResourceClickable : MonoBehaviour, IPointerClickHandler
    {
        private Resource resource;

        private void Awake()
        {
            resource = GetComponent<Resource>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var harvestTool = ServiceCenter.Get<IItemService>().TakeHarvestTool(resource, transform.position);
            var canUse = harvestTool.TryUse(out var explanation);
            if (!canUse)
            {
                UIManager.Instance.ShowMessage(explanation);
            }
        }
    }
}