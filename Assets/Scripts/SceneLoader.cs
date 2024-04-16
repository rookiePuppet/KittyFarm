using System;
using System.Collections;
using Framework;
using UnityEngine.SceneManagement;

namespace KittyFarm
{
    public class SceneLoader : MonoSingleton<SceneLoader>
    {
        public static event Action<int> MapLoaded;

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
                    MapLoaded?.Invoke(mapId);
                }));
        }

        private IEnumerator LoadSceneAsync(string name, Action onSceneLoaded = null)
        {
            yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            onSceneLoaded?.Invoke();
        }
    }
}