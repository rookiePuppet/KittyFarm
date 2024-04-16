using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class StartView : UIBase
    {
        [SerializeField] private Button startButton;

        private void Start()
        {
            startButton.onClick.AddListener(() =>
            {
                SceneLoader.Instance.LoadMapScene(0);
                SceneManager.UnloadSceneAsync("StartScene");
                
                UIManager.Instance.HideUI<StartView>();
                
                UIManager.Instance.ShowUI<GameView>();
                UIManager.Instance.ShowUI<OnScreenControllerView>();
            });
        }
    }
}
