using System;
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

        public static void SaveData(object data, string fileName)
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

        public static T LoadData<T>(string fileName)
        {
            var filePath = GetFilePath(fileName);
            switch (saveType)
            {
                case SaveType.PlayerPrefs when PlayerPrefs.HasKey(filePath):
                {
                    var json = PlayerPrefs.GetString(filePath);
                    return JsonUtility.FromJson<T>(json);
                }
                case SaveType.JsonFile when File.Exists(filePath):
                {
                    var json = File.ReadAllText(filePath);
                    return JsonUtility.FromJson<T>(json);
                }
                default:
                    // 数据文件不存在，使用反射创建新的数据实例
                    return CreateDataInstance<T>();
            }
        }

        private static T CreateDataInstance<T>()
        {
            var data = Activator.CreateInstance<T>();
            foreach (var field in typeof(T).GetFields())
            {
                field.SetValue(data, Activator.CreateInstance(field.FieldType));
            }

            return data;
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