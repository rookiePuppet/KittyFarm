using System.Collections.Generic;
using KittyFarm.Data;
using KittyFarm.Service;

namespace KittyFarm.MapClick
{
    public class FarmProduct : UsableItem
    {
        public FarmProduct(UsableItemSet set) : base(set)
        {
            JudgeConditions = new List<JudgeCondition>
            {
                new(() => !ServiceCenter.Get<ITilemapService>().IsNotDroppableAt(itemSet.CellPosition),
                    $"不能将{itemSet.ItemData.ItemName}丢在这里"),
                new(() => itemSet.MeetDistanceAtWorld, "走近点试试吧")
            };
        }

        protected override List<JudgeCondition> JudgeConditions { get; }

        protected override void Use()
        {
            itemSet.ItemService.SpawnItemAt(itemSet.WorldPosition, itemSet.ItemData);
            GameDataCenter.Instance.PlayerInventory.RemoveItem(itemSet.ItemData);
        }
    }
}