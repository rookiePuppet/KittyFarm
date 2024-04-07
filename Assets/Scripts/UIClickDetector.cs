using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KittyFarm
{
    public class UIClickDetector : MonoSingleton<UIClickDetector>
    {
        private GraphicRaycaster graphicRaycaster;
        
        protected override void Awake()
        {
            base.Awake();
            graphicRaycaster = FindObjectOfType<GraphicRaycaster>();
        }

        public bool CheckIfClickOnUI(Vector2 position)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = position
            };
            var raycastHits = new List<RaycastResult>();
            graphicRaycaster.Raycast(eventData, raycastHits);
            
            return raycastHits.Count > 0;
        }
    }
}