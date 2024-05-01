using System.Linq;
using KittyFarm.Service;
using KittyFarm.Time;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.CropSystem
{
    public class CropClickable : MonoBehaviour, IPointerClickHandler
    {
        private Crop crop;

        private void Awake()
        {
            crop = GetComponent<Crop>();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            UIManager.Instance.GetUI<GameView>().ShowCropInfoBoard(GetCropInfo());
            
            var harvestTool = ServiceCenter.Get<IItemService>().TakeHarvestTool(crop, transform.position);
            var judgements = harvestTool.JudgeUsable().ToArray();
            if (judgements.Length > 0)
            {
                UIManager.Instance.ShowMessage(judgements[0]);
                return;
            }
            
            harvestTool.Use();
        }
        
        private CropInfo GetCropInfo()
        {
            ServiceCenter.Get<ICropService>().GrowthTracker.UpdateSingleCrop(crop);
            
            var cropData = crop.Data;
            var cropGrowthDetails = crop.GrowthDetails;  
            var growthTime = TimeManager.GetTimeSpanFrom(cropGrowthDetails.PlantedTime);
            return new CropInfo
            {
                CropName = cropData.CropName,
                Stage = cropGrowthDetails.CurrentStage,
                GrowthTime = growthTime,
                RipeRate = (float)growthTime.TotalMinutes / cropData.TotalMinutesToBeRipe
            };
        }
    }
}