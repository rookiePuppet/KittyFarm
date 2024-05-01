using System.Linq;
using KittyFarm.Service;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.CropSystem
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
            var judgements = harvestTool.JudgeUsable().ToArray();
            if (judgements.Length > 0)
            {
                UIManager.Instance.ShowMessage(judgements[0]);
                return;
            }
            
            harvestTool.Use();
        }
    }
}