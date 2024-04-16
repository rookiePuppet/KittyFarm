using System;
using System.IO;
using UnityEngine;

namespace FrameWork
{
    public static class JsonDataManager
    {
        private static string DataPath => Application.persistentDataPath;

        public static void SaveData(object data, string filePath)
        {
            var path = $"{DataPath}/{filePath}.json";
            var json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
        }

        public static T LoadData<T>(string filePath)
        {
            T data;
            
            var path = $"{DataPath}/{filePath}.json";
            // 数据文件已存在，从Json中读取
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                data = JsonUtility.FromJson<T>(json);
            }
            // 数据文件不存在，使用反射创建新的数据实例
            else
            {
                data = Activator.CreateInstance<T>();
                foreach (var field in typeof(T).GetFields())
                {
                    field.SetValue(data, Activator.CreateInstance(field.FieldType));
                }
            }

            return data;
        }
    }
}