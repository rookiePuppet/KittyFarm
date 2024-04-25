using KittyFarm.Service;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    public abstract class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceDataSO data;
        
        public ResourceDataSO Data => data;
        public ResourceGrowthDetails GrowthDetails { get; set; }
        public bool CanBeHarvested => !IsGrowing;
        protected bool IsGrowing { get; private set; }

        public virtual void Refresh()
        {
            var sinceLastCollectTime = TimeManager.GetTimeSpanFrom(GrowthDetails.LastCollectTime);
            IsGrowing = sinceLastCollectTime.TotalHours < data.RegenerationTime;
        }

        public virtual void Collect()
        {
            if (IsGrowing) return;
            
            GrowthDetails.LastCollectTime = TimeManager.CurrentTime;
            Refresh();

            ServiceCenter.Get<IItemService>().SpawnItemAt(transform.position, data.ProductData, 3);
        }
    }
}