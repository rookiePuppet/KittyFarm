using System.IO;
using UnityEngine;

namespace FrameWork
{
    public static class JsonDataManager
    {
        private enum SaveType
        {
            JsonFile,
            PlayerPrefs,
        }

        private static readonly SaveType saveType = SaveType.JsonFile;

        public static void SaveData(string fileName, object data)
        {
            var json = JsonUtility.ToJson(data);
            if (saveType == SaveType.PlayerPrefs)
            {
                PlayerPrefs.SetString(GetFilePath(fileName), json);
            }
            else
            {
                File.WriteAllText(GetFilePath(fileName), json);
            }
        }

        public static void LoadData<T>(string fileName, out T data) where T : ScriptableObject
        {
            var filePath = GetFilePath(fileName);

            data = ScriptableObject.CreateInstance<T>();

            var json = saveType switch
            {
                SaveType.JsonFile => File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty,
                SaveType.PlayerPrefs => PlayerPrefs.HasKey(filePath) ? PlayerPrefs.GetString(filePath) : string.Empty,
                _ => string.Empty
            };
            
            if (json == string.Empty) return;
            JsonUtility.FromJsonOverwrite(json, data);
        }

        public static bool Exists<T>(string fileName)
        {
            var filePath = GetFilePath(fileName);

            return saveType switch
            {
                SaveType.JsonFile => File.Exists(filePath),
                SaveType.PlayerPrefs => PlayerPrefs.HasKey(filePath),
                _ => false
            };
        }

        private static string GetFilePath(string fileName)
        {
            return saveType switch
            {
                SaveType.JsonFile => $"{Application.persistentDataPath}/{fileName}.json",
                SaveType.PlayerPrefs => fileName,
                _ => fileName
            };
        }
    }
}