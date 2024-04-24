using UnityEngine;

namespace KittyFarm.UI
{
    public abstract class UIBase : MonoBehaviour
    {
        protected bool IsVisible { get; private set; }
        
        public virtual void Show()
        {
            gameObject.SetActive(true);
            IsVisible = true;
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            IsVisible = false;
        }
    }
}