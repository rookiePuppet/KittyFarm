using System;
using KittyFarm.Data;
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

        public abstract void Collect();

        protected void FinishCollection()
        {
            GrowthDetails.LastCollectTime = TimeManager.CurrentTime;
            Refresh();

            var itemService = ServiceCenter.Get<IItemService>();
            foreach (var position in Data.ProductPositions)
            {
                itemService.SpawnItemAt(transform, position, data.ProductData);
            }
        }
    }
}