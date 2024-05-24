using KittyFarm.Data;
using KittyFarm.MapClick;
using KittyFarm.Service;
using KittyFarm.Time;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.InteractiveObject
{
    public class CropClickable : MonoBehaviour, IPointerClickHandler
    {
        private Crop crop;

        private HandItem HandItem => GameManager.Player.HandItem;

        private void Awake()
        {
            crop = GetComponent<Crop>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            UIManager.Instance.GetUI<GameView>().ShowCropInfoBoard(GetCropInfo());

            if (HandItem.Is(ItemType.Basket))
            {
                var basket = ServiceCenter.Get<IItemService>().TakeUsableItem(HandItem.Current,
                    transform.position, crop.GrowthDetails.CellPosition) as Basket;
                basket.CollectTarget = crop;
                var canUse = basket.TryUse(out var explanation);
                if (!canUse)
                {
                    UIManager.Instance.ShowMessage(explanation);
                }
            }
            else
            {
                UIManager.Instance.ShowMessage("需要使用篮子收获");
            }
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