using System;
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
            if (resource.CanBeHarvested)
            {
                resource.Collect();
            }
        }
    }
}