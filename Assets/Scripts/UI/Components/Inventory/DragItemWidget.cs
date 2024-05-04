using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class DragItemWidget: UIBase
    {
        public Vector3 Position
        {
            get => dragImage.rectTransform.position;
            set => dragImage.rectTransform.position = value;
        }
        
        public Sprite ItemSprite
        {
            set => dragImage.sprite = value;
        }
        
        private Image dragImage;

        private void Awake()
        {
            dragImage = GetComponent<Image>();
        }
    }
}