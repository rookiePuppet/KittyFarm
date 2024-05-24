using UnityEngine;

namespace KittyFarm.InteractiveObject
{
    [CreateAssetMenu(fileName = "NewTreeData", menuName = "Data/Tree Data")]
    public class TreeDataSO: ScriptableObject
    {
        public ProductDetails BranchDetails;
        public ProductDetails WoodDetails;
        public float RandomRangeRadius = 1f;
        public ResourceGrowthStage[] Stages;

        public Vector2 RandomProductPosition {
            get
            {
                var position = Vector2.zero;
                position.x = Random.Range(-RandomRangeRadius, RandomRangeRadius);
                position.y = Random.Range(-RandomRangeRadius, RandomRangeRadius);
                return position;
            }
        }
    }
}