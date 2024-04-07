using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Search;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.UIElements;

public class SlicedSpritesNameGenerator : EditorWindow
{
    private List<Sprite> sprites;
    private ObjectField toProcessFolderField;

    [MenuItem("Tools/Sliced Sprites Name Generator")]
    public static void OpenWindow()
    {
        GetWindow<SlicedSpritesNameGenerator>("Sliced Sprites Name Generator");
    }

    private void CreateGUI()
    {
        var label = new Label("重新按顺序生成切片精灵名称")
        {
            style =
            {
                unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.Bold)
            }
        };
        rootVisualElement.Add(label);
        
        toProcessFolderField = new ObjectField("target folder");
        rootVisualElement.Add(toProcessFolderField);
        
        var generateButton = new Button(GenerateNamesForAllSprites) { text = "Generate" };
        rootVisualElement.Add(generateButton);
    }

    private void GenerateNamesForAllSprites()
    {
        var folderPath = AssetDatabase.GetAssetPath(toProcessFolderField.value);
        var directory = new DirectoryInfo(folderPath);
        var fileInfos = directory.GetFiles("*.png");

        foreach (var fileInfo in fileInfos)
        {
            GenerateNamesForSprite(fileInfo);
        }
        
        AssetDatabase.Refresh();
    }

    private void GenerateNamesForSprite(FileInfo fileInfo)
    {
        var assetPath = EditorUtil.GetAssetPath(fileInfo.FullName);
        var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;

        var factory = new SpriteDataProviderFactories();
        var dataProvider = factory.GetSpriteEditorDataProviderFromObject(importer);
        dataProvider.InitSpriteEditorDataProvider();

        var prefix = Path.GetFileNameWithoutExtension(fileInfo.Name);
            
        var index = 0;
        var spriteRects = dataProvider.GetSpriteRects();
        foreach (var item in spriteRects)
        {
            item.name = $"{prefix}_{index++}";
        }
        dataProvider.SetSpriteRects(spriteRects);
        dataProvider.Apply();
            
        Debug.Log("已处理：" + prefix);
    }
}