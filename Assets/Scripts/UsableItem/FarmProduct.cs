using System.Collections.Generic;
using KittyFarm.Map;

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
            
            if (!itemSet.MeetDistanceAtWorld)
            {
                judgementList.Add("走近点试试");
            }
            
            return judgementList;
        }
        
    }
}