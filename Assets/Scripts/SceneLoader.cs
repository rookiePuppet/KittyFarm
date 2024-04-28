using System;
using System.Collections;
using Framework;
using UnityEngine.SceneManagement;

namespace KittyFarm
{
    public class SceneLoader : MonoSingleton<SceneLoader>
    {
        public static event Action MapLoaded;

        public void LoadMapScene(string sceneName, Action onSceneLoaded = null, Action beforeLoadScene = null)
        {
            beforeLoadScene?.Invoke();
            
            StartCoroutine(LoadSceneAsync(sceneName,
                () =>
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
                    MapLoaded?.Invoke();
                    
                    onSceneLoaded?.Invoke();
                }));
        }

        private IEnumerator LoadSceneAsync(string sceneName, Action onSceneLoaded = null)
        {
            if (!SceneManager.GetSceneByName(sceneName).IsValid())
            {
                yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }

            onSceneLoaded?.Invoke();
        }
    }
}