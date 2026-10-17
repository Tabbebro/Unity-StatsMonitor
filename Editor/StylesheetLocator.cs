using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine.UIElements;

static class StylesheetLocator {
    public static StyleSheet Load([CallerFilePath] string callerPath = "") {
        var folder = Path.GetDirectoryName(callerPath)?.Replace('\\', '/');
        if (string.IsNullOrEmpty(folder)) {
            return null;
        }

        var assetPath = ToProjectRelativePath(folder) + "/styles.uss";

        //return AssetDatabase.LoadAssetAtPath<StyleSheet>(assetPath);

        var sheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(assetPath);

        // Log The Tried Path So Failures Are Diagnosable
        if (sheet == null) {
            Debug.LogError($"StylesheetLocator: no stylesheet at '{assetPath}' (caller: {callerPath})");
        }

        return sheet;
    }

    static string ToProjectRelativePath(string absoluteFolder) {
        /*
        if (absoluteFolder.StartsWith("./")) {
            absoluteFolder = absoluteFolder.Substring(2);
        }

        if (absoluteFolder.StartsWith("Assets/") || absoluteFolder.StartsWith("Packages/")) {
            return absoluteFolder;
        }
        */

        absoluteFolder = Path.GetFullPath(absoluteFolder).Replace('\\', '/');

        var dataPath = Application.dataPath.Replace('\\', '/');
        if (IsUnder(absoluteFolder, dataPath)) {
            return "Assets" + absoluteFolder.Substring(dataPath.Length);
        }

        // Pick the longest matching package root to avoid prefix collisions
        string bestResult = null;
        var bestLength = -1;
        foreach (var package in UnityEditor.PackageManager.PackageInfo.GetAllRegisteredPackages()) {
            var resolvedPath = package.resolvedPath.Replace('\\', '/');
            if (IsUnder(absoluteFolder, resolvedPath) && resolvedPath.Length > bestLength) {
                bestLength = resolvedPath.Length;
                bestResult = package.assetPath + absoluteFolder.Substring(resolvedPath.Length);
            }

        }

        return bestResult ?? absoluteFolder;
    }

    static bool IsUnder(string path, string root) {
        return path.Equals(root, System.StringComparison.OrdinalIgnoreCase) || path.StartsWith(root + "/", System.StringComparison.OrdinalIgnoreCase);
    }
}