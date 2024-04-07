using KittyFarm.Map;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridPropertiesDataGenerator))]
public class GridPropertiesDataGeneratorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var generator = (GridPropertiesDataGenerator)target;
        if (GUILayout.Button("Generate Grid Properties Data"))
        {
            generator.GenerateData();
        }
    }
}