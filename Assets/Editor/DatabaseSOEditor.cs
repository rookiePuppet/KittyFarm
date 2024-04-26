using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using KittyFarm.Data;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DatabaseSO<>), true)]
public class DatabaseSOEditor : Editor
{
    public DefaultAsset folder;

    private Type targetType;
    private Type dataListItemType;
    private FieldInfo dataListField;
    private MethodInfo addMethod;

    private SerializedProperty folderProperty;
    private new SerializedObject serializedObject;

    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        folderProperty = serializedObject.FindProperty(nameof(folder));

        targetType = target.GetType();
        dataListItemType = targetType.BaseType.GenericTypeArguments[0];
        var dataListType = typeof(List<>).MakeGenericType(dataListItemType);
        addMethod = dataListType.GetMethod("Add");
        dataListField = targetType.GetField("dataList", BindingFlags.NonPublic | BindingFlags.Instance);
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUI.BeginChangeCheck();
        folderProperty.objectReferenceValue = EditorGUILayout.ObjectField("Data folder",
            folderProperty.objectReferenceValue, typeof(DefaultAsset), true);

        if (GUILayout.Button("Collect Data"))
        {
            CollectDataFromFolder();
        }

        if (GUILayout.Button("Auto Numbering"))
        {
            NumberingForDataItems();
        }

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
        DrawDefaultInspector();
    }

    private void NumberingForDataItems()
    {
        var idField = dataListItemType.GetField("Id");
        var dataList = dataListField.GetValue(target);

        var index = 1;
        foreach (var item in (IEnumerable)dataList)
        {
            idField.SetValue(item, index);
            index++;
        }
        
        Debug.Log($"{target.name}自动编号已完成");
    }

    private void CollectDataFromFolder()
    {
        if (folderProperty.objectReferenceValue == null) return;

        var folderQueue = new Queue<string>();

        var folderPath = AssetDatabase.GetAssetPath(folderProperty.objectReferenceValue);
        folderQueue.Enqueue(folderPath);

        var dataList = dataListField.GetValue(target);
        while (folderQueue.Count != 0)
        {
            folderPath = folderQueue.Dequeue();
            Debug.Log($"处理文件夹：{folderPath}");

            var files = Directory.GetFiles(folderPath, "*.asset");
            foreach (var file in files)
            {
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(file);
                addMethod.Invoke(dataList, new object[] { asset });
            }

            foreach (var subFolderPath in Directory.GetDirectories(folderPath))
            {
                folderQueue.Enqueue(subFolderPath);
            }
        }
    }
}