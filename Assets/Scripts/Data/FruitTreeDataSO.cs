using KittyFarm.Data;
using UnityEngine;

namespace KittyFarm.InteractiveObject
{
    [CreateAssetMenu(fileName = "NewFruitTreeData", menuName = "Data/Fruit Tree Data")]
    public class FruitTreeDataSO : TreeDataSO
    {
        public ProductDetails FruitDetails;
        public Vector2[] FixedProductPositions;
    }
}