using System.Collections.Generic;
using KittyFarm.InteractiveObject;

namespace KittyFarm.MapClick
{
    public class Basket : UsableItem
    {
        public ICollectable CollectTarget { private get; set; }
        
        protected override List<JudgeCondition> JudgeConditions { get; }
        
        public Basket(UsableItemSet set) : base(set)
        {
            JudgeConditions = new List<JudgeCondition>
            {
                new(() => CollectTarget.CanBeCollected, "还没成熟呢，晚点再看看吧"),
                new(() => itemSet.MeetDistanceAtWorld, "走近点试试吧")
            };
        }
        
        protected override void Use()
        {
            CollectTarget.Collect();
        }
    }
}