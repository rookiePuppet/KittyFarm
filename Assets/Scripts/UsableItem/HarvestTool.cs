using System.Collections.Generic;
using KittyFarm.Harvestable;

namespace KittyFarm.MapClick
{
    public class HarvestTool : UsableItem
    {
        public IHarvestable HarvestTarget { private get; set; }

        public HarvestTool(UsableItemSet set) : base(set)
        {
            JudgeConditions = new List<JudgeCondition>
            {
                new(() => HarvestTarget.CanBeHarvested, "还没成熟呢，晚点再看看吧"),
                new(() => itemSet.MeetDistanceAtWorld, "走近点试试吧")
            };
        }

        protected override List<JudgeCondition> JudgeConditions { get; }

        protected override void Use()
        {
            HarvestTarget.Harvest();
        }
    }
}