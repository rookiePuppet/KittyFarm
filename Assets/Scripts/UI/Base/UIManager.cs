using System.Collections.Generic;
using Framework;
using UnityEngine;

namespace KittyFarm.UI
{
    public enum UILayer
    {
        Top,
        Middle,
        Bottom
    }

    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private string uiRootPath = "UI";
        [Space]
        [SerializeField] private Canvas topCanvas;
        [SerializeField] private Canvas middleCanvas;
        [SerializeField] private Canvas bottomCanvas;

        private readonly Dictionary<string, UIBase> uiDic = new();
        private GameMessagePool messagePool;

        protected override void Awake()
        {
            base.Awake();

            InitializeMessagePool();
        }

        public TUI ShowUI<TUI>(UILayer layer = UILayer.Middle) where TUI : UIBase
        {
            var uiName = typeof(TUI).Name;
            var targetCanvas = CanvasAt(layer);

            if (!uiDic.TryGetValue(uiName, out var ui))
            {
                var path = GetUIPath(uiName);
                var uiObj = Instantiate(Resources.Load<GameObject>(path), targetCanvas.transform);
                ui = uiObj.GetComponent<TUI>();
                uiDic[uiName] = ui;
            }
    
            // 让UI显示在目标层级的最上面
            var uiCountInCanvas = targetCanvas.transform.childCount;
            if (uiCountInCanvas > 1 && ui.transform.GetSiblingIndex() < uiCountInCanvas - 1)
            {
                ui.transform.SetSiblingIndex(uiCountInCanvas - 1);
            }
            
            ui.Show();

            return ui as TUI;
        }

        public void HideUI<TUI>() where TUI : UIBase
        {
            var uiName = typeof(TUI).Name;

            if (uiDic.TryGetValue(uiName, out var ui))
            {
                ui.Hide();
            }
        }

        public void SetAllCanvasVisible(bool visible)
        {
            topCanvas.gameObject.SetActive(visible);
            middleCanvas.gameObject.SetActive(visible);
            bottomCanvas.gameObject.SetActive(visible);
        }

        public void DestroyUI<TUI>() where TUI : UIBase
        {
            var uiName = typeof(TUI).Name;

            if (uiDic.TryGetValue(uiName, out var ui))
            {
                Destroy(ui.gameObject);
            }
        }

        public TUI GetUI<TUI>() where TUI : UIBase
        {
            var uiName = typeof(TUI).Name;
            if (!uiDic.TryGetValue(uiName, out var ui)) return null;

            return ui as TUI;
        }

        public void ClearCache()
        {
            foreach (var ui in uiDic.Values)
            {
                Destroy(ui.gameObject);
            }

            uiDic.Clear();
            messagePool.Clear();
        }

        public void ShowMessage(string content)
        {
            var message = messagePool.Get();
            message.Show(content);
        }

        private void InitializeMessagePool()
        {
            var path = GetUIPath(nameof(GameMessage));
            var prefab = Resources.Load<GameObject>(path);

            var poolParent = new GameObject(nameof(GameMessagePool));
            poolParent.transform.SetParent(CanvasAt(UILayer.Top).transform);
            poolParent.transform.localPosition = Vector3.zero;

            messagePool = poolParent.AddComponent<GameMessagePool>();
            messagePool.Initialize(prefab, CanvasAt(UILayer.Top).transform);
        }

        private string GetUIPath(string uiName)
        {
            return $"{uiRootPath}/{uiName}";
        }

        private Canvas CanvasAt(UILayer layer) => layer switch
        {
            UILayer.Top => topCanvas,
            UILayer.Middle => middleCanvas,
            UILayer.Bottom => bottomCanvas,
            _ => bottomCanvas
        };
    }
}