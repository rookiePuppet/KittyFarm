using System.Collections.Generic;
using KittyFarm.Data;

namespace KittyFarm.MapClick
{
    public class Hand : UsableItem
    {
        protected override List<JudgeCondition> JudgeConditions { get; } = new()
        {
            new JudgeCondition(() => UsableItemSet.MeetDistanceAt(WorldPosition, ItemType.None), "你的小手可没那么长"),
        };

        protected override void Use()
        {
        }
    }
}