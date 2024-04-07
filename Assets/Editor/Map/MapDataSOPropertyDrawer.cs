using System.Reflection;
using KittyFarm.CropSystem;
using KittyFarm.Map;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(MapDataSO))]
public class MapDataSOPropertyDrawer : Editor
{
    private new MapDataSO target;

    public override VisualElement CreateInspectorGUI()
    {
        target = (MapDataSO)serializedObject.targetObject;

        var container = new VisualElement();

        // 地图名称
        var mapNameField = new PropertyField(serializedObject.FindProperty("mapName"));
        container.Add(mapNameField);

        // 起始坐标
        var originCoordinateField = new PropertyField(serializedObject.FindProperty("gridOriginCoordinate"));
        container.Add(originCoordinateField);

        // 地图大小
        var gridSizeField = new PropertyField(serializedObject.FindProperty("gridSize"));
        container.Add(gridSizeField);

        // 属性数据
        var propertiesDataRow = new VisualElement
        {
            style = { flexDirection = FlexDirection.Row }
        };
        var propertiesDataField = new PropertyField(serializedObject.FindProperty("propertiesData"));
        var createPropertiesDataButton = new Button(CreatePropertiesData)
        {
            text = "Create",
            style = { alignSelf = Align.FlexEnd }
        };
        propertiesDataRow.Add(propertiesDataField);
        propertiesDataRow.Add(createPropertiesDataButton);
        container.Add(propertiesDataRow);

        // 作物数据
        var cropsDataRow = new VisualElement
        {
            style = { flexDirection = FlexDirection.Row }
        };
        var cropsDataField = new PropertyField(serializedObject.FindProperty("cropsData"));
        var createCropsDataButton = new Button(CreateCropsData)
        {
            text = "Create",
            style = { alignSelf = Align.FlexEnd }
        };
        cropsDataRow.Add(cropsDataField);
        cropsDataRow.Add(createCropsDataButton);
        container.Add(cropsDataRow);

        // 瓦片数据
        var tilesDataRow = new VisualElement
        {
            style = { flexDirection = FlexDirection.Row }
        };
        var tilesDataField = new PropertyField(serializedObject.FindProperty("tilesData"));
        var createTilesDataButton = new Button(CreateTilesData)
        {
            text = "Create",
            style = { alignSelf = Align.FlexEnd }
        };
        tilesDataRow.Add(tilesDataField);
        tilesDataRow.Add(createTilesDataButton);
        container.Add(tilesDataRow);

        // 重命名按钮
        var renameButton = new Button(RenameAsset)
        {
            text = "Rename by MapName",
            style = { marginTop = 20 }
        };
        container.Add(renameButton);

        return container;
    }

    private void CreatePropertiesData()
    {
        if (target.PropertiesData != null)
        {
            Debug.LogWarning("瓦片属性数据已赋值，若要重新创建，请先删除原有数据");
            return;
        }

        var newPropertiesData = CreateInstance<MapPropertiesDataSO>();

        var propertiesDataField =
            typeof(MapDataSO).GetField("propertiesData", BindingFlags.NonPublic | BindingFlags.Instance);
        propertiesDataField?.SetValue(target, newPropertiesData);

        var rootPath = EditorUtil.GetAssetRootPath(AssetDatabase.GetAssetPath(target));
        var path = $"{rootPath}/MapPropertiesData{{{target.MapName}}}.asset";
        AssetDatabase.CreateAsset(newPropertiesData, path);
    }

    private void CreateCropsData()
    {
        if (target.CropsData != null)
        {
            Debug.LogWarning("作物数据已赋值，若要重新创建，请先删除原有数据");
            return;
        }

        var newCropsData = CreateInstance<MapCropsDataSO>();

        var cropsDataField =
            typeof(MapDataSO).GetField("cropsData", BindingFlags.NonPublic | BindingFlags.Instance);
        cropsDataField?.SetValue(target, newCropsData);

        var rootPath = EditorUtil.GetAssetRootPath(AssetDatabase.GetAssetPath(target));
        var path = $"{rootPath}/MapCropsData{{{target.MapName}}}.asset";
        AssetDatabase.CreateAsset(newCropsData, path);
    }

    private void CreateTilesData()
    {
        if (target.TilesData != null)
        {
            Debug.LogWarning("瓦片数据已赋值，若要重新创建，请先删除原有数据");
            return;
        }

        var newTilesData = CreateInstance<MapTilesDataSO>();

        var tilesDataField =
            typeof(MapDataSO).GetField("tilesData", BindingFlags.NonPublic | BindingFlags.Instance);
        tilesDataField?.SetValue(target, newTilesData);

        var rootPath = EditorUtil.GetAssetRootPath(AssetDatabase.GetAssetPath(target));
        var path = $"{rootPath}/MapTilesData{{{target.MapName}}}.asset";
        AssetDatabase.CreateAsset(newTilesData, path);
    }

    private void RenameAsset()
    {
        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(target), $"MapData{{{target.MapName}}}");
    }
}