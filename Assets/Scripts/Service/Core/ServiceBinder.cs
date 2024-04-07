using System;
using UnityEngine;

namespace KittyFarm.Service
{
    public class ServiceBinder : MonoBehaviour
    {
        private event Action<string> MapChanged;
        
        private void Awake()
        {
            var mapService = GetComponentInChildren<MapService>();
            var cropService = GetComponentInChildren<CropService>();
            var itemService = GetComponentInChildren<ItemService>();

            var pointerService = GetComponentInChildren<PointerService>();
            
            ServiceCenter.Register<IMapService>(mapService);
            ServiceCenter.Register<ICropService>(cropService);
            ServiceCenter.Register<IItemService>(itemService);
            ServiceCenter.Register<IPointerService>(pointerService);
            
            MapChanged += (mapName) =>
            {
                mapService.Initialize(mapName);
                cropService.LoadCropsOnMap(mapService.MapData.CropsData);
            };
        }

        private void OnEnable()
        {
            SceneLoader.Instance.MapLoaded += MapChanged;
        }
    }
}