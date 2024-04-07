using UnityEngine;

namespace KittyFarm.UI
{
    [CreateAssetMenu(fileName = "ObjectPropertyConfiguration", menuName = "Kitty Farm/ObjectPropertyConfiguration")]
    public class ObjectPropertyConfigurationSO: ScriptableObject
    {
        public Sprite[] CircleSprites;
        public Sprite TrueSprite;
        public Sprite FalseSprite;
    }
}