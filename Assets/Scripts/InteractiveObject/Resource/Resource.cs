using KittyFarm.Data;
using KittyFarm.Service;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.InteractiveObject
{
    public abstract class Resource : MonoBehaviour, ICollectable
    {
        [SerializeField] private ResourceDataSO data;
        
        public ResourceDataSO Data => data;
        public ResourceGrowthDetails GrowthDetails { get; set; }
        public abstract bool CanBeCollected { get; protected set; }
        
        protected bool IsGrowing { get; private set; }
        
        public abstract void Collect();

        public virtual void Refresh()
        {
            var sinceLastCollectTime = TimeManager.GetTimeSpanFrom(GrowthDetails.LastCollectTime);
            IsGrowing = sinceLastCollectTime.TotalHours < data.RegenerationTime;
        }
        
        protected void FinishCollection()
        {
            GrowthDetails.LastCollectTime = TimeManager.CurrentTime;
            Refresh();

            var itemService = ServiceCenter.Get<IItemService>();
            var basePosition = transform.position;
            foreach (var offset in Data.ProductPositions)
            {
                // itemService.SpawnItemAt(basePosition + offset, data.ProductData);
            }
        }
    }
}