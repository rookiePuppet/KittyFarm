using System.Threading.Tasks;
using Framework;
using UnityEngine.SceneManagement;

namespace KittyFarm
{
    public class SceneLoader : MonoSingleton<SceneLoader>
    {
        public static async Task<Scene> LoadSceneAsync(SceneName sceneName, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            await UnityLoadSceneAsync(sceneName, mode);
            
            var scene = SceneManager.GetSceneByName(sceneName.ToString());
            SceneManager.SetActiveScene(scene);
            
            return scene;
        }
        
        private static Task UnityLoadSceneAsync(SceneName sceneName, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            var tcs = new TaskCompletionSource<bool>();
        
            var loadOperation = SceneManager.LoadSceneAsync(sceneName.ToString(), mode);
            loadOperation.completed += _ => { tcs.SetResult(true); };
            
            return tcs.Task;
        }

        public static Task UnityUnloadSceneAsync(Scene scene)
        {
            var tcs = new TaskCompletionSource<bool>();
        
            var loadOperation = SceneManager.UnloadSceneAsync(scene);
            loadOperation.completed += _ => { tcs.SetResult(true); };
            
            return tcs.Task;
        }
        
        // public static async Task<Scene> LoadSceneAndSetActiveAsync(string sceneName)
        // {
        //     // 由于加载模式是Additive，执行完同步加载场景方法后
        //     // 虽然场景已经显示，但仍未完全加载完成（isLoaded属性为false），不能设置为激活场景
        //     SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        //     
        //     var scene = SceneManager.GetSceneByName(sceneName);
        //     // 等待场景完全加载完成
        //     await WaitForSceneCompletelyLoaded(scene);
        //     
        //     SceneManager.SetActiveScene(scene);
        //     
        //     return scene;
        // }
        //
        // public static async Task<Scene> LoadSceneAsync(string sceneName)
        // {
        //     await UnityLoadSceneAsync(sceneName);
        //
        //     var scene = SceneManager.GetSceneByName(sceneName);
        //     SceneManager.SetActiveScene(scene);
        //
        //     return scene;
        // }
        //
        
        // private static async Task WaitForSceneCompletelyLoaded(Scene scene)
        // {
        //     var tcs = new TaskCompletionSource<bool>();
        //     
        //     var sceneLoaded = false;
        //     while (!sceneLoaded)
        //     {
        //         await Task.Delay(100);
        //         sceneLoaded = scene.isLoaded;
        //     }
        //     
        //     tcs.SetResult(true);
        // }
    }
}