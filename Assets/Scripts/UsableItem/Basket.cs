using System.Collections.Generic;
using KittyFarm.InteractiveObject;

namespace KittyFarm.MapClick
{
    public class Basket : UsableItem
    {
        public static ICollectable CollectTarget { private get; set; }
        
        protected override List<JudgeCondition> JudgeConditions { get; } = new()
        {
            new JudgeCondition (() => CollectTarget != null, "没什么可装的"),
            new JudgeCondition(() => CollectTarget.CanBeCollected, "还没成熟呢，晚点再看看吧"),
            new JudgeCondition(() => UsableItemSet.MeetDistanceAt(WorldPosition, ItemData.Type), "走近点试试吧")
        };
        
        protected override void Use()
        {
            CollectTarget.Collect();
        }
    }
}