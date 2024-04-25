using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.CropSystem
{
    public class FruitTree : Resource
    {
        private SpriteRenderer fruitRenderer;

        private void Awake()
        {
            fruitRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        public override void Refresh()
        {
            base.Refresh();

            fruitRenderer.enabled = !IsGrowing;
        }

        public override void Collect()
        {
            base.Collect();
            
            print($"收获了{Data.ResourceName}");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (CanBeHarvested)
            {
                Collect();
            }
            else
            {
                print("不能收获，还在生长中！");
            }
        }
    }
}