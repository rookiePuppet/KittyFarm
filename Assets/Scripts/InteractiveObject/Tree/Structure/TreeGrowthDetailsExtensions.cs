using System;
using KittyFarm.Time;

namespace KittyFarm.InteractiveObject
{
    public static class TreeGrowthDetailsExtensions
    {
        public static int CalculateCurrentStage(this TreeGrowthDetails growthDetails, TreeDataSO treeData)
        {
            var stageIndex = 0;
            var growthDuration = TimeManager.GetTimeSpanFrom(growthDetails.PlantedTime).TotalMinutes;
            foreach (var stage in treeData.Stages)
            {
                var stageMinutes = new TimeSpan(stage.GrowthHours, stage.GrowthMinutes, 0).TotalMinutes;
                if (growthDuration >= stageMinutes)
                {
                    stageIndex++;
                    growthDuration -= stageMinutes;
                }
                else
                {
                    break;
                }
            }

            return Math.Min(stageIndex, treeData.Stages.Length - 1);
        }

        public static void ChangePlantedTime(this TreeGrowthDetails growthDetails, TreeDataSO treeData, int stageIndex)
        {
            if (stageIndex < 0 || stageIndex >= treeData.Stages.Length)
            {
                return;
            }
            
            var growthDuration = TimeSpan.Zero;
            for (var i = 0; i < stageIndex; i++)
            {
                var stage = treeData.Stages[i];
                growthDuration += new TimeSpan(stage.GrowthHours, stage.GrowthMinutes, 0);
            }
            
            // 因为TimeManager不会实时同步毫秒时间，在计算状态索引时会有误差，导致索引计算错误，所以这里加1分钟
            growthDetails.PlantedTime = TimeManager.CurrentTime - (growthDuration + TimeSpan.FromMinutes(1));
        }
    }
}