using System.Collections.Generic;
using KittyFarm.Data;

namespace KittyFarm
{
    public class Seed : UsableItem
    {
        public Seed(UsableItemSet set) : base(set)
        {
        }
        
        public override void Use()
        {
            itemSet.CropService.PlantCrop(((SeedDataSO)itemSet.ItemData).CropData, itemSet.CellPosition);
        }

        public override IEnumerable<string> JudgeUsable()
        {
            judgementList.Clear();
            
            var wasDugAtCell = itemSet.TilemapService.CheckWasDugAt(itemSet.CellPosition);
            var existsCropAtCell = itemSet.CropService.IsCropExistentAt(itemSet.CellPosition);

            if (!itemSet.MeetDistanceAtCellCenter)
            {
                judgementList.Add("走近点试试");
            }
            
            if (!wasDugAtCell)
            {
                judgementList.Add("还没有松土呢");
            }
            
            if (existsCropAtCell)
            {
                judgementList.Add("这里已经没位置种了哦");
            }

            return judgementList;
        }
    }
}