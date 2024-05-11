using UnityEngine;

namespace KittyFarm
{
    [CreateAssetMenu(fileName = "ObjectOcclusionFaderConfig", menuName = "Object Occlusion Fader Config")]
    public class ObjectOcclusionFaderConfigSO : ScriptableObject
    {
        public float fadeAlpha = 0.2f;
        public float fadeDuration = 0.6f;
    }
}