using System;
using System.Collections.Generic;

namespace KittyFarm.MapClick
{
    public abstract class UsableItem
    {
        protected abstract List<JudgeCondition> JudgeConditions { get; }
        protected readonly UsableItemSet itemSet;

        protected UsableItem(UsableItemSet set)
        {
            itemSet = set;
        }

        protected abstract void Use();

        public bool TryUse(out string explanation)
        {
            if (!JudgeUsable(out explanation))
            {
                return false;
            }

            Use();
            return true;
        }

        protected bool JudgeUsable(out string explanation)
        {
            foreach (var condition in JudgeConditions)
            {
                if (condition.ExecuteJudgeAction()) continue;

                explanation = condition.FalseExplanation;
                return false;
            }

            explanation = string.Empty;
            return true;
        }
    }

    public readonly struct JudgeCondition
    {
        public readonly string FalseExplanation;
        private readonly Func<bool> judge;

        public JudgeCondition(Func<bool> judge, string falseExplanation)
        {
            FalseExplanation = falseExplanation;
            this.judge = judge;
        }

        public bool ExecuteJudgeAction()
        {
            return judge.Invoke();
        }
    }
}