using System;
using UnityEngine;

namespace KittyFarm.Service
{
    public class ServiceBinder : MonoBehaviour
    {
        private void Awake()
        {
            var mapService = GetComponent<TilemapService>();
            var cropService = GetComponent<CropService>();
            var itemService = GetComponent<ItemService>();
            var pointerService = GetComponent<PointerService>();
            
            ServiceCenter.Register<ITilemapService>(mapService);
            ServiceCenter.Register<ICropService>(cropService);
            ServiceCenter.Register<IItemService>(itemService);
            ServiceCenter.Register<IPointerService>(pointerService);
        }
    }
}