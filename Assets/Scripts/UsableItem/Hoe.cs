using System.Collections.Generic;

namespace KittyFarm.MapClick
{
    public class Hoe : UsableItem
    {
        protected override List<JudgeCondition> JudgeConditions { get; }
        private PlayerAnimation PlayerAnimation => GameManager.Player.Animation;

        private bool isUsing;
        
        public Hoe(UsableItemSet set) : base(set)
        {
            JudgeConditions = new List<JudgeCondition>
            {
                new(()=> !isUsing, "别急，我的动作没那么快呀"),
                new(() => itemSet.TilemapService.IsPlantableAt(itemSet.CellPosition), "这里不能种植哦"),
                new(() => !itemSet.TilemapService.CheckWasDugAt(itemSet.CellPosition), "这里已经松过土了"),
                new(() => itemSet.MeetDistanceAtCellCenter, "走进点试试吧")
            };
        }
        
        protected override async void Use()
        {
            isUsing = true;
            InputReader.DisableInput();

            var playerPosition = GameManager.Player.transform.position;
            var actionDirection = (itemSet.CellCenterPosition - playerPosition).normalized;
            await PlayerAnimation.PlayUseTool(actionDirection, ToolType.Hoe,
                () =>
                {
                    AudioManager.Instance.PlaySoundEffect(GameSoundEffect.Dig);
                    itemSet.TilemapService.DigAt(itemSet.CellPosition);
                });

            InputReader.EnableInput();

            isUsing = false;
        }
    }
}