using System.Collections.Generic;
using KittyFarm.CropSystem;

namespace KittyFarm
{
    public class HarvestTool : UsableItem
    {
        public IHarvestable HarvestTarget { private get; set; }

        public HarvestTool(UsableItemSet set) : base(set)
        {
        }

        public override void Use()
        {
            HarvestTarget.Harvest();
        }

        public override IEnumerable<string> JudgeUsable()
        {
            judgementList.Clear();
            
            if (!itemSet.MeetDistanceAtWorld)
            {
                judgementList.Add("走近点试试吧");
            }

            if (!HarvestTarget.CanBeHarvested)
            {
                judgementList.Add("还没成熟呢，晚点再看看吧");
            }

            return judgementList;
        }
    }
}