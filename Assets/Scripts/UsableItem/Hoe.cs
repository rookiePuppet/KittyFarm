using System.Collections.Generic;
using KittyFarm.Map;

namespace KittyFarm
{
    public class Hoe : UsableItem
    {
        public Hoe(UsableItemSet set) : base(set)
        {
        }
        
        private PlayerAnimation PlayerAnimation => GameManager.Player.Animation;

        public override async void Use()
        {
            InputReader.DisableInput();

            var playerPosition = GameManager.Player.transform.position;
            var actionDirection = (itemSet.CellCenterPosition - playerPosition).normalized;
            await PlayerAnimation.PlayUseTool(actionDirection, ToolType.Hoe,
                () => { itemSet.TilemapService.DigAt(itemSet.CellPosition); });

            InputReader.EnableInput();
        }

        public override IEnumerable<string> JudgeUsable()
        {
            judgementList.Clear();

            var isPlantable = itemSet.TilemapService.IsPlantableAt(itemSet.CellPosition);
            var wasDug = itemSet.TilemapService.CheckWasDugAt(itemSet.CellPosition);
            
            if (!itemSet.MeetDistanceAtCellCenter)
            {
                judgementList.Add("走近点试试吧");
            }
            
            if (!isPlantable)
            {
                judgementList.Add("这里不能种植哦");
            }

            if (wasDug)
            {
                judgementList.Add("这里已经松过土了哦");
            }
            
            return judgementList;
        }
    }
}