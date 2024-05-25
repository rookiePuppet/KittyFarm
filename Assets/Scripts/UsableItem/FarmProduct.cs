using System.Collections.Generic;
using KittyFarm.Data;
using KittyFarm.Service;

namespace KittyFarm.MapClick
{
    public class FarmProduct : UsableItem
    {
        protected override List<JudgeCondition> JudgeConditions { get; }= new List<JudgeCondition>
        {
            new(() => !ServiceCenter.Get<ITilemapService>().IsNotDroppableAt(CellPosition),
                $"不能将{ItemData.ItemName}丢在这里"),
            new(() => UsableItemSet.MeetDistanceAt(WorldPosition, ItemData.Type), "走近点试试吧")
        };

        protected override void Use()
        {
            ServiceCenter.Get<IItemService>().SpawnItemAt(WorldPosition, ItemData);
            GameDataCenter.Instance.PlayerInventory.RemoveItem(ItemData);
        }
    }
}