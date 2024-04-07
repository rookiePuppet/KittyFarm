using System;
using Framework;

namespace KittyFarm
{
    public class SceneLoader : MonoSingleton<SceneLoader>
    {
        public event Action<string> MapLoaded;

        public void LoadMapScene(string mapName)
        {
            MapLoaded?.Invoke(mapName);
        }
    }
}