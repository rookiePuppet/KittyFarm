using System.Collections.Generic;

namespace KittyFarm
{
    public class UndefinedUsageItem: UsableItem
    {
        public UndefinedUsageItem(UsableItemSet set) : base(set)
        {
        }

        public override void Use()
        {
            
        }

        public override IEnumerable<string> JudgeUsable()
        {
            judgementList.Clear();
            judgementList.Add("未定义用途");
            return judgementList;
        }
    }
}