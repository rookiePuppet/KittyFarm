using System;

public static class EditorUtil
{
    public static string GetAssetRootPath(string assetPath) =>
        assetPath[..assetPath.LastIndexOf("/", StringComparison.Ordinal)];

    public static string GetAssetPath(string filePath) =>
        filePath[filePath.IndexOf("Assets", StringComparison.Ordinal)..];
}