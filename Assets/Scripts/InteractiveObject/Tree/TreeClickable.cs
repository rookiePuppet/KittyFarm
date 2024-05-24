using KittyFarm.Data;
using KittyFarm.MapClick;
using KittyFarm.Service;
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
            var currentHandItem = GameManager.Player.HandItem.Current;
            if (currentHandItem != null && currentHandItem.Type == ItemType.Axe)
            {
                var axe = (Axe)ServiceCenter.Get<IItemService>().TakeUsableItem(currentHandItem, transform.position, Vector3Int.zero);
                axe.Target = isFruitTree ? fruitTree : tree;
                if (axe.TryUse(out var explanation))
                {
                    UIManager.Instance.ShowMessage(explanation);
                }
            }
            else if(isFruitTree)
            {
                fruitTree.OnOtherClicked();
            }
            else
            {
                tree.OnOtherClicked();
            }
        }
    }
}