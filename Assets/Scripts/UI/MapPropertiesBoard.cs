using DG.Tweening;
using UnityEngine;

namespace KittyFarm.UI
{
    public class MapPropertiesBoard : UIBase
    {
        [SerializeField] private Sprite trueSprite;
        [SerializeField] private Sprite falseSprite;
        
        [SerializeField] private TilePropertyEntry tilePlantableProperty;
        [SerializeField] private TilePropertyEntry tileDroppableProperty;

        private RectTransform rectTransform => (RectTransform)transform;

        public override void Show()
        {
            var isVisibleBefore = IsVisible;
            
            base.Show();

            if (isVisibleBefore) return;
            
            var position = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(position.x, 0);
            rectTransform.DOAnchorPosY(position.y, 0.5f);
        }

        public void Refresh(TilePropertiesInfo info)
        {
            tilePlantableProperty.SetValueSprite(SpriteFromValue(info.IsTilePlantable));
            tileDroppableProperty.SetValueSprite(SpriteFromValue(info.IsTileDroppable));
        }

        private Sprite SpriteFromValue(bool value) => value switch
        {
            true => trueSprite,
            false => falseSprite
        };
    }
}