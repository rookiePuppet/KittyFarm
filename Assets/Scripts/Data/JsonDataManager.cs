using System.IO;
using Framework;
using UnityEngine;

namespace KittyFarm.Data
{
    public class JsonDataManager : Singleton<JsonDataManager>
    {
        private string DataPath => Application.persistentDataPath;

        public void SaveData<T>(T data)
        {
            var json = JsonUtility.ToJson(data);
            File.WriteAllText($"{DataPath}/{typeof(T).Name}.json", json);
        }

        public T LoadData<T>()
        {
            var path = $"{DataPath}/{typeof(T).Name}.json";

            if (File.Exists(path))
            {
                var json = File.ReadAllText($"{DataPath}/{typeof(T).Name}.json");
                var data = JsonUtility.FromJson<T>(json);
                return data;
            }
            else
            {
                return default;
            }
        }
    }
}