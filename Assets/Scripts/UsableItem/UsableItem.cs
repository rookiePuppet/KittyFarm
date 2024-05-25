using System;
using System.Collections.Generic;
using KittyFarm.Data;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm.MapClick
{
    public abstract class UsableItem
    {
        protected static ItemDataSO ItemData { get; private set; }
        protected static Vector3 WorldPosition{ get; private set; }
        protected static Vector3Int CellPosition{ get; private set; }
        protected static Vector3 CellCenterPosition => ServiceCenter.Get<ITilemapService>().GetCellCenterWorld(CellPosition);
        
        protected abstract List<JudgeCondition> JudgeConditions { get; }

        public void Initialize(ItemDataSO itemData, Vector3 worldPosition, Vector3Int cellPosition)
        {
            ItemData = itemData;
            WorldPosition = worldPosition;
            CellPosition = cellPosition;
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