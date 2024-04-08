using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class TilePropertyEntry : MonoBehaviour
    {
        private Image valueImage;

        private void Awake()
        {
            valueImage = transform.Find("ValueImage").GetComponent<Image>();
        }

        public void SetValueSprite(Sprite sprite)
        {
            valueImage.sprite = sprite;
        }
    }
}