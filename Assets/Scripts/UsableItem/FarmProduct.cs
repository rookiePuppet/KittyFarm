using System.Collections.Generic;

namespace KittyFarm
{
    public class FarmProduct: UsableItem
    {
        public FarmProduct(UsableItemSet set) : base(set)
        {
        }
        
        public override void Use()
        {
            itemSet.ItemService.SpawnItemAt(itemSet.WorldPosition, itemSet.ItemData);
        }

        public override IEnumerable<string> JudgeUsable()
        {
            judgementList.Clear();

            // if (ServiceCenter.Get<ITilemapService>().IsNotDroppableAt(itemSet.CellPosition))
            // {
            //     judgementList.Add($"不能将{itemSet.ItemData.ItemName}丢在这里");
            //     return judgementList;
            // }
            
            if (!itemSet.MeetDistanceAtWorld)
            {
                judgementList.Add("走近点试试");
            }
            
            return judgementList;
        }
        
    }
}