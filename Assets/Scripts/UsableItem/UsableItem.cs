using System.Collections.Generic;

namespace KittyFarm
{
    public abstract class UsableItem
    {
        protected readonly List<string> judgementList = new();
        
        protected readonly UsableItemSet itemSet;

        protected UsableItem(UsableItemSet set)
        {
            itemSet = set;
        }
        
        public abstract void Use();
        
        public abstract IEnumerable<string> JudgeUsable();
    }
}