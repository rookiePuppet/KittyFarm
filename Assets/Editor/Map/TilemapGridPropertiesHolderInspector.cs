using KittyFarm.Map;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TilemapGridPropertiesHolder))]
public class TilemapGridPropertiesHolderInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var holder = (TilemapGridPropertiesHolder)target;

        if (GUILayout.Button("Load Grid Properties"))
        {
            holder.LoadGridProperties();
        }
    }
}