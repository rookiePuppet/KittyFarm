using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using ObjectField = UnityEditor.UIElements.ObjectField;

public class RuleTileSpritesLoader : EditorWindow
{
    private VisualTreeAsset spriteListEntryTemplate;

    private RuleTile TargetRuleTile => ruleTileField.value as RuleTile;
    private Texture2D TileTexture => textureField.value as Texture2D;

    private readonly List<Sprite> sprites = new();

    private ObjectField ruleTileField;
    private ObjectField textureField;

    private ListView spritesList;

    [MenuItem("Tools/Rule Tile Sprites Loader")]
    public static void OpenWindow()
    {
        GetWindow<RuleTileSpritesLoader>("Rule Tile Sprites Loader");
    }

    private void OnEnable()
    {
        spriteListEntryTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
            "Assets/Editor/RuleTile/uxml/sprite_preview_object_field_template.uxml");

        if (spriteListEntryTemplate is null)
        {
            Debug.LogError("template not found");
        }
    }

    private void CreateGUI()
    {
        // 目标瓦片选择
        ruleTileField = new ObjectField
        {
            label = "Target Rule Tile",
            objectType = typeof(RuleTile)
        };
        rootVisualElement.Add(ruleTileField);

        // 目标瓦片集贴图选择
        textureField = new ObjectField
        {
            label = "The Texture contains the sprites you want to load",
            objectType = typeof(Texture2D)
        };
        rootVisualElement.Add(textureField);

        var layout = new VisualElement
        {
            style =
            {
                flexDirection = FlexDirection.Row
            }
        };

        // 加载瓦片精灵按钮
        var showSpritesButton = new Button { text = "Show sprites in the list below" };
        showSpritesButton.clicked += ShowSprites;

        // 载入瓦片精灵到目标瓦片按钮
        var loadSpritesButton = new Button { text = "Load sprites below into the target rule tile" };
        loadSpritesButton.clicked += LoadSprites;

        layout.Add(showSpritesButton);
        layout.Add(loadSpritesButton);

        rootVisualElement.Add(new Button(ReorderTileByNumber)
        {
            text = "Reorder Tile"
        });

        rootVisualElement.Add(layout);

        // 显示当前瓦片集贴图中的所有瓦片精灵的列表
        spritesList = new ListView
        {
            makeItem = () => spriteListEntryTemplate.Instantiate(),
            bindItem = (item, index) =>
            {
                var sprite = sprites[index];
                var spriteFiled = item.Q<ObjectField>("SpriteField");
                spriteFiled.label = sprite.name;
                spriteFiled.value = sprite;

                item.Q<VisualElement>("SpritePreview").style.backgroundImage = new StyleBackground(sprite);
            },
            itemsSource = sprites,
            showAddRemoveFooter = true,
            showFoldoutHeader = true,
            headerTitle = "Tile Sprites",
            fixedItemHeight = 50f,
            selectionType = SelectionType.Multiple
        };

        rootVisualElement.Add(spritesList);
    }

    private void ReorderTileByNumber()
    {
        var dic = new Dictionary<int, RuleTile.TilingRule>();
        var maxNumber = 0;
        foreach (var tilingRule in TargetRuleTile.m_TilingRules)
        {
            var number = GetNumberFrom(tilingRule.m_Sprites[0].name);
            maxNumber = Math.Max(maxNumber, number);
            dic.Add(number, tilingRule);
        }

        var newTilingRules = new List<RuleTile.TilingRule>();
        for (var index = 0; index <= maxNumber; index++)
        {
            if (dic.TryGetValue(index, out var tilingRule))
            {
                newTilingRules.Add(tilingRule);
            }
        }

        TargetRuleTile.m_TilingRules = newTilingRules;
        
        EditorUtility.SetDirty(TargetRuleTile);
    }

    private void ReorderSpritesByNumber(List<Sprite> sprites)
    {
        var dic = new Dictionary<int, Sprite>();
        var maxNumber = 0;
        foreach (var sprite in sprites)
        {
            var number = GetNumberFrom(sprite.name);
            maxNumber = Math.Max(maxNumber, number);
            dic.Add(number, sprite);
        }
        
        sprites.Clear();
        for (var index = 0; index <= maxNumber; index++)
        {
            if (dic.TryGetValue(index, out var sprite))
            {
                sprites.Add(sprite);
            }
        }
    }

    private int GetNumberFrom(string spriteName) =>
        int.Parse(spriteName[(spriteName.LastIndexOf("_", StringComparison.Ordinal) + 1)..]);

    private void ShowSprites()
    {
        var texturePath = AssetDatabase.GetAssetPath(TileTexture);
        var allAssets = AssetDatabase.LoadAllAssetsAtPath(texturePath);

        spritesList.itemsSource = null;
        
        sprites.Clear();
        sprites.Capacity = allAssets.Length;
        
        foreach (var asset in allAssets)
        {
            if (asset is Sprite sprite && asset != null)
            {
                sprites.Add(sprite);
            }
        }
        
        ReorderSpritesByNumber(sprites);

        spritesList.itemsSource = sprites;
    }

    private void LoadSprites()
    {
        if (TargetRuleTile == null)
        {
            Debug.LogError("请选择目标规则瓦片！");
            return;
        }

        if (sprites.Count == 0)
        {
            Debug.LogError("即将自动载入的瓦片列表为空，请检查！");
            return;
        }

        if (TargetRuleTile.m_TilingRules.Count == 0)
        {
            var tilingRules = new List<RuleTile.TilingRule>();
            foreach (var sprite in sprites)
            {
                tilingRules.Add(new RuleTile.TilingRule()
                {
                    m_Sprites = new[] { sprite }
                });
            }

            TargetRuleTile.m_TilingRules = tilingRules;
        }
        else
        {
            var tilingRules = TargetRuleTile.m_TilingRules;
            var index = 0;
            foreach (var sprite in sprites)
            {
                if (index >= tilingRules.Count) break;
                tilingRules[index].m_Sprites = new[] { sprite };
                index++;
            }
        }
        
        EditorUtility.SetDirty(TargetRuleTile);

        Debug.Log("载入已完成。");
    }
}