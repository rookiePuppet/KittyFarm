using System;
using UnityEngine;

namespace KittyFarm.Service
{
    public class ServiceBinder : MonoBehaviour
    {
        private void Awake()
        {
            var mapService = GetComponentInChildren<TilemapService>();
            var cropService = GetComponentInChildren<CropService>();
            var itemService = GetComponentInChildren<ItemService>();

            var pointerService = GetComponentInChildren<PointerService>();
            
            ServiceCenter.Register<ITilemapService>(mapService);
            ServiceCenter.Register<ICropService>(cropService);
            ServiceCenter.Register<IItemService>(itemService);
            ServiceCenter.Register<IPointerService>(pointerService);
        }
    }
}