using System;
using System.Collections;
using Framework;
using UnityEngine.SceneManagement;

namespace KittyFarm
{
    public class SceneLoader : MonoSingleton<SceneLoader>
    {
        public static event Action MapLoaded;

        private void Start()
        {
            SceneManager.LoadScene("StartScene", LoadSceneMode.Additive);
        }

        public void LoadMapScene(int mapId)
        {
            StartCoroutine(LoadSceneAsync("Plain",
                () =>
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Plain")); 
                    MapLoaded?.Invoke();
                }));
        }

        private IEnumerator LoadSceneAsync(string name, Action onSceneLoaded = null)
        {
            yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            onSceneLoaded?.Invoke();
        }
    }
}