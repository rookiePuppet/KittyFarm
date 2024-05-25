using System.Collections.Generic;
using KittyFarm.Data;
using KittyFarm.Service;

namespace KittyFarm.MapClick
{
    public class Seed : UsableItem
    {
        protected override List<JudgeCondition> JudgeConditions { get; }= new()
        {
            new JudgeCondition(() => ServiceCenter.Get<ITilemapService>().IsPlantableAt(CellPosition), "这里不能种植哦"),
            new JudgeCondition(() => ServiceCenter.Get<ITilemapService>().CheckWasDugAt(CellPosition), "还没有松土呢"),
            new JudgeCondition(() => !ServiceCenter.Get<ICropService>().IsCropExistentAt(CellPosition), "这里已经没位置种了哦"),
            new JudgeCondition(() => UsableItemSet.MeetDistanceAt(CellCenterPosition, ItemData.Type), "走近点试试吧")
        };
        
        protected override void Use()
        {
            ServiceCenter.Get<ICropService>().PlantCrop(((SeedDataSO)ItemData).CropData, CellPosition);
            AudioManager.Instance.PlaySoundEffect(GameSoundEffect.PlantSeed);
            GameDataCenter.Instance.PlayerInventory.RemoveItem(ItemData);
        }
    }
}