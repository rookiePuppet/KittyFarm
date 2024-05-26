using KittyFarm.Data;
using KittyFarm.MapClick;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.InteractiveObject
{
    public class TreeClickable : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private bool isFruitTree;

        private Tree tree;
        private FruitTree fruitTree;

        private void Awake()
        {
            if (isFruitTree)
            {
                fruitTree = GetComponent<FruitTree>();
            }
            else
            {
                tree = GetComponent<Tree>();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var handItem = GameManager.Player.HandItem;
            if (handItem.Is(ItemType.Axe))
            {
                var axe = UsableItemSet.TakeUsableItem(handItem.Current, transform.position, Vector3Int.zero);
                Axe.Target = isFruitTree ? fruitTree : tree;
                if (!axe.TryUse(out var explanation))
                {
                    UIManager.Instance.ShowMessage(explanation);
                }
            }
            else
            {
                var hand = UsableItemSet.TakeUsableItem(null, transform.position, Vector3Int.zero);
                if (hand.TryUse(out var handExplanation))
                {
                    if (isFruitTree)
                    {
                        fruitTree.OnOtherClicked();
                    }
                    else
                    {
                        tree.OnOtherClicked();
                    }
                }
                else
                {
                    UIManager.Instance.ShowMessage(handExplanation);
                }
            }
            
            Axe.Target = null;
        }
    }
}