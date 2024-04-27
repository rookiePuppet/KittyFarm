using DG.Tweening;
using UnityEngine;

namespace KittyFarm
{
    public class ObjectOcclusionFader : MonoBehaviour
    {
        [SerializeField] private ObjectOcclusionFaderConfigSO faderConfig;

        private SpriteRenderer[] spriteRenderers;

        private void Awake()
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                FadeOut();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                FadeIn();
            }
        }

        private void FadeOut()
        {
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.DOFade(faderConfig.fadeAlpha, faderConfig.fadeDuration);
            }
        }

        private void FadeIn()
        {
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.DOFade(1f, faderConfig.fadeDuration);
            }
        }
    }
}