using System;
using System.Drawing;
using System.IO;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class SlicedSpriteProcessor : EditorWindow
{
    private ObjectField textureField;
    private ObjectField folderField;
    private Texture2D TargetTexture => textureField.value as Texture2D;

    [MenuItem("Tools/Sliced Sprite Processor")]
    public static void ShowWindow()
    {
        GetWindow<SlicedSpriteProcessor>();
    }

    private void CreateGUI()
    {
        textureField = new ObjectField
        {
            objectType = typeof(Texture2D),
            label = "The single texture to process"
        };

        rootVisualElement.Add(textureField);

        folderField = new ObjectField
        {
            objectType = typeof(DefaultAsset),
            label = "The folder contains all textures to process"
        };
        rootVisualElement.Add(folderField);

        var processSingleButton = new Button(() => ProcessTexture(true))
        {
            text = "Process single texture"
        };
        rootVisualElement.Add(processSingleButton);

        var processAllButton = new Button(() => ProcessTexture(false))
        {
            text = "Process all textures in the folder"
        };
        rootVisualElement.Add(processAllButton);
    }

    private void ProcessTexture(bool singleTexture)
    {
        var path = EditorUtility.SaveFolderPanel("保存路径", "", "");

        if (singleTexture) ProcessSingleTexture(TargetTexture, path);
        else ProcessAllTexturesInFolder(path);
    }

    private void ProcessAllTexturesInFolder(string rootPath)
    {
        if (folderField.value == null)
        {
            Debug.Log("The folder is null, please select one first");
            return;
        }


        var folderPath = AssetDatabase.GetAssetPath(folderField.value);
        var directory = new DirectoryInfo(folderPath);
        ProcessAllTexturesInFolder(directory, rootPath);
        Debug.Log("处理完成");
    }

    private void ProcessAllTexturesInFolder(DirectoryInfo rootDirectory, string rootPath)
    {
        var subDirectories = rootDirectory.GetDirectories();
        Debug.Log(rootPath);

        foreach (var directory in subDirectories)
        {
            var fileInfos = directory.GetFiles("*.png");

            var currentRootPath = $"{rootPath}/{directory.Name}";

            foreach (var fileInfo in fileInfos)
            {
                var assetPath = EditorUtil.GetAssetPath(fileInfo.FullName);
                var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
                try
                {
                    ProcessSingleTexture(texture, currentRootPath);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Exception occurred while processing texture: {texture.name}");
                    throw;
                }
            }

            ProcessAllTexturesInFolder(directory, currentRootPath);
        }
    }

    private void ProcessSingleTexture(Texture2D texture, string rootPath)
    {
        var importer = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(texture)) as TextureImporter;

        if (importer == null)
        {
            Debug.LogError($"Can not find the texture '{texture.name}'");
        }
        else if (importer.isReadable is false)
        {
            Debug.LogError($"Please enable Read/Write for '{texture.name}' in the texture import settings");
            return;
        }
        else if (importer.spriteImportMode == SpriteImportMode.Single)
        {
            Debug.LogWarning($"The texture is a single sprite, {texture.name}");
            return;
        }

        var provider = new SpriteDataProviderFactories().GetSpriteEditorDataProviderFromObject(importer);
        provider.InitSpriteEditorDataProvider();

        var spriteRects = provider.GetSpriteRects();

        var outputPath = $"{rootPath}/{texture.name}";
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        foreach (var spriteRect in spriteRects)
        {
            SaveSlicedImageToFile(texture, spriteRect.rect, spriteRect.name, rootPath);
        }
    }

    private void SaveSlicedImageToFile(Texture2D texture, Rect rect, string imageName, string rootPath)
    {
        var width = (int)rect.width;
        var height = (int)rect.height;

        var pixels = texture.GetPixels((int)rect.x, (int)rect.y, width, height);
        var image = new Bitmap(width, height);

        var index = 0;
        foreach (var pixel in pixels)
        {
            var color = System.Drawing.Color.FromArgb(
                (int)(pixel.a * 255),
                (int)(pixel.r * 255),
                (int)(pixel.g * 255),
                (int)(pixel.b * 255)
            );
            // 从左上角开始
            image.SetPixel(index % width, height - 1 - index / width, color);

            index++;
        }

        Debug.Log($"{rootPath}/{texture.name}/{imageName}.png");
        
        image.Save($"{rootPath}/{texture.name}/{imageName}.png", System.Drawing.Imaging.ImageFormat.Png);
    }
}