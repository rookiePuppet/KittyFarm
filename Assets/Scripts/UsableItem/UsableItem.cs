using System.Collections.Generic;
using KittyFarm.Map;

namespace KittyFarm
{
    public abstract class UsableItem
    {
        protected readonly UsableItemSet itemSet;
        
        protected readonly List<string> judgementList = new();

        protected UsableItem(UsableItemSet set)
        {
            itemSet = set;
        }
        
        public abstract void Use();
        public abstract IEnumerable<string> JudgeUsable();
    }
}