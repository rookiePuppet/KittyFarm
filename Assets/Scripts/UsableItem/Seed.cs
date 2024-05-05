using System.Collections.Generic;
using KittyFarm.Data;

namespace KittyFarm
{
    public class Seed : UsableItem
    {
        protected override List<JudgeCondition> JudgeConditions { get; }

        public Seed(UsableItemSet set) : base(set)
        {
            JudgeConditions = new List<JudgeCondition>
            {
                new(() => itemSet.TilemapService.CheckWasDugAt(itemSet.CellPosition), "还没有松土呢"),
                new(() => !itemSet.CropService.IsCropExistentAt(itemSet.CellPosition), "这里已经没位置种了哦"),
                new(() => itemSet.MeetDistanceAtCellCenter, "走近点试试吧")
            };
        }

        protected override void Use()
        {
            itemSet.CropService.PlantCrop(((SeedDataSO)itemSet.ItemData).CropData, itemSet.CellPosition);
        }
    }
}