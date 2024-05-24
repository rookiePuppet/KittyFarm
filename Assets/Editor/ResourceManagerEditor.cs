using KittyFarm.Data;
using KittyFarm.InteractiveObject;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceManager))]
public class ResourceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Save Resources Data"))
        {
            var mapResourcesData =
                (MapResourcesDataSO)serializedObject.FindProperty("mapResourcesData").objectReferenceValue;
            mapResourcesData.ResourcesDetailsList.Clear();

            foreach (var resource in FindObjectsOfType<Resource>())
            {
                var growthDetails = new ResourceGrowthDetails()
                {
                    Position = resource.transform.position,
                    ResourceId = resource.Data.Id,
                    LastCollectTimeTicks = 0
                };
                mapResourcesData.AddGrowthDetails(growthDetails);
            }
            
            EditorUtility.SetDirty(mapResourcesData);
        }
    }
}