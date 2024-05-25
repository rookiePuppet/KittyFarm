using System.Collections.Generic;
using KittyFarm.InteractiveObject;

namespace KittyFarm.MapClick
{
    public class Axe : UsableItem
    {
        public IChopped Target { get; set; }
        
        protected override List<JudgeCondition> JudgeConditions => new()
        {
            new JudgeCondition(() => Target != null, "找找树在哪？"),
            new JudgeCondition(()=>  Target.CanBeChopped, "这玩意儿不能砍"),
            new JudgeCondition(() => UsableItemSet.MeetDistanceAt(WorldPosition, ItemData.Type), "走近点试试吧"),
            new JudgeCondition(()=> !PlayerAnimation.IsUsingTool, "别急，我的动作没那么快呀"),
        }; 
        
        private static PlayerAnimation PlayerAnimation => GameManager.Player.Animation;
        
        protected override async void Use()
        {
            InputReader.DisableInput();

            var playerPosition = GameManager.Player.transform.position;
            var actionDirection = (WorldPosition - playerPosition).normalized;
            await PlayerAnimation.PlayUseTool(actionDirection, ToolType.Axe,
                () =>
                {
                    AudioManager.Instance.PlaySoundEffect(GameSoundEffect.Chop);
                    Target.OnChopped();
                });

            Target = null;
            InputReader.EnableInput();
        }
    }
}