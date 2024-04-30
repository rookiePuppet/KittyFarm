using System.Collections.Generic;
using Framework;
using UnityEngine;

namespace KittyFarm.UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private string canvasTag = "MainCanvas";
        [SerializeField] private string uiRootPath = "UI";

        private readonly Dictionary<string, UIBase> uiDic = new();

        private Canvas canvas;
        private GameMessagePool messagePool;

        protected override void Awake()
        {
            base.Awake();
            
            canvas = GameObject.FindWithTag(canvasTag).GetComponent<Canvas>();
            InitializeMessagePool();
        }

        public TUI ShowUI<TUI>() where TUI : UIBase
        {
            var uiName = typeof(TUI).Name;

            if (!uiDic.TryGetValue(uiName, out var ui))
            {
                var path = GetUIPath(uiName);
                var uiObj = Instantiate(Resources.Load<GameObject>(path), canvas.transform);
                ui = uiObj.GetComponent<TUI>();
                uiDic[uiName] = ui;
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
            poolParent.transform.SetParent(canvas.transform);
            poolParent.transform.localPosition = Vector3.zero;

            messagePool = poolParent.AddComponent<GameMessagePool>();
            messagePool.Initialize(prefab, canvas.transform);
        }

        private string GetUIPath(string uiName)
        {
            return $"{uiRootPath}/{uiName}";
        }
    }
}