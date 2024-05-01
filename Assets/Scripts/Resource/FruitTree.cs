using UnityEngine;

namespace KittyFarm.CropSystem
{
    public class FruitTree : Resource
    {
        private SpriteRenderer fruitRenderer;

        private Animator treeAnimator;
        private Animator fruitAnimator;
        
        private static readonly int AnimatorHash_Shake = Animator.StringToHash("Shake");
        private static readonly int AnimatorHash_Fall = Animator.StringToHash("Fall");

        private void Awake()
        {
            var child = transform.GetChild(0);
            fruitRenderer = child.GetComponent<SpriteRenderer>();
            treeAnimator = GetComponent<Animator>();
            fruitAnimator = child.GetComponent<Animator>();
        }

        public override void Refresh()
        {
            base.Refresh();

            fruitRenderer.enabled = !IsGrowing;
        }

        public override void Harvest()
        {
            treeAnimator.SetTrigger(AnimatorHash_Shake);
            fruitAnimator.SetTrigger(AnimatorHash_Fall);
        }
    }
}