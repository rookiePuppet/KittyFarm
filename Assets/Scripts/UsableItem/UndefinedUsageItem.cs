using System.Collections.Generic;

namespace KittyFarm
{
    public class UndefinedUsageItem : UsableItem
    {
        public UndefinedUsageItem(UsableItemSet set) : base(set)
        {
            JudgeConditions = new List<JudgeCondition>
            {
                new(() => false, "未定义用途")
            };
        }

        protected override List<JudgeCondition> JudgeConditions { get; }

        protected override void Use()
        {
        }
    }
}