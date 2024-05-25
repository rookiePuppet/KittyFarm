using System.Collections.Generic;
using KittyFarm.Service;

namespace KittyFarm.MapClick
{
    public class Hoe : UsableItem
    {
        protected override List<JudgeCondition> JudgeConditions { get; }= new()
        {
            new JudgeCondition(()=> !PlayerAnimation.IsUsingTool, "别急，我的动作没那么快呀"),
            new JudgeCondition(() =>ServiceCenter.Get<ITilemapService>().IsPlantableAt(CellPosition), "这里不能种植哦"),
            new JudgeCondition(() => !ServiceCenter.Get<ITilemapService>().CheckWasDugAt(CellPosition), "这里已经松过土了"),
            new JudgeCondition(() => UsableItemSet.MeetDistanceAt(CellCenterPosition, ItemData.Type), "走进点试试吧")
        };
        
        private static PlayerAnimation PlayerAnimation => GameManager.Player.Animation;
        
        protected override async void Use()
        {
            InputReader.DisableInput();

            var playerPosition = GameManager.Player.transform.position;
            var actionDirection = (CellCenterPosition - playerPosition).normalized;
            await PlayerAnimation.PlayUseTool(actionDirection, ToolType.Hoe,
                () =>
                {
                    AudioManager.Instance.PlaySoundEffect(GameSoundEffect.Dig);
                    ServiceCenter.Get<ITilemapService>().DigAt(CellPosition);
                });

            InputReader.EnableInput();
        }
    }
}