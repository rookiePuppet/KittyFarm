using System;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    public class FruitTree: MonoBehaviour
    {
        
        
        private SpriteRenderer treeRenderer;
        private SpriteRenderer fruitRenderer;

        private void Awake()
        {
            treeRenderer = GetComponent<SpriteRenderer>();
            fruitRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
}