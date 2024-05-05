using KittyFarm.Data;
using KittyFarm.Service;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    public interface IHarvestable
    {
        public bool CanBeHarvested { get; }
        public void Harvest();
    }
    
    public abstract class Resource : MonoBehaviour, IHarvestable
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

        public abstract void Harvest();

        protected void FinishCollection()
        {
            GrowthDetails.LastCollectTime = TimeManager.CurrentTime;
            Refresh();

            var itemService = ServiceCenter.Get<IItemService>();
            var basePosition = transform.position;
            foreach (var offset in Data.ProductPositions)
            {
                itemService.SpawnItemAt(basePosition + offset, data.ProductData);
            }
        }
    }
}