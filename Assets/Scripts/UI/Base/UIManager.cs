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

        protected override void Awake()
        {
            base.Awake();
            canvas = GameObject.FindWithTag(canvasTag).GetComponent<Canvas>();
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

        public TUI GetUI<TUI>() where TUI : UIBase
        {
            var uiName = typeof(TUI).Name;
            if (!uiDic.TryGetValue(uiName, out var ui)) return null;
            
            return ui as TUI;
        }

        private string GetUIPath(string uiName)
        {
            return $"{uiRootPath}/{uiName}";
        }
    }
}