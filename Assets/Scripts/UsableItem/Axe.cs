using System.Collections.Generic;
using KittyFarm.InteractiveObject;

namespace KittyFarm.MapClick
{
    public class Axe : UsableItem
    {
        public IChopped Target { get; set; }
        
        public Axe(UsableItemSet set) : base(set)
        {
        }

        protected override List<JudgeCondition> JudgeConditions => new()
        {
            new JudgeCondition(() => itemSet.MeetDistanceAtWorld, "走近点试试吧")
        };
        
        protected override void Use()
        {
            Target.OnChopped();
        }
    }
}