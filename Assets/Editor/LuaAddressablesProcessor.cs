using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class LuaAddressablesProcessor : Editor
{
    // 定义常量，方便修改
    private const string LUA_SOURCE_FOLDER = "Assets/Lua";
    private const string LUA_TARGET_FOLDER = "Assets/LuaTxt";
    private const string LUA_ADDRESSABLE_GROUP = "LuaScripts";

    [MenuItem("Tools/Lua/Process Lua for Addressables")]
    public static void ProcessLuaFiles()
    {
        string sourceFolderPath = Path.Combine(Directory.GetCurrentDirectory(), LUA_SOURCE_FOLDER);
        if (!Directory.Exists(sourceFolderPath))
        {
            Debug.LogError($"Lua source folder not found at: {sourceFolderPath}");
            return;
        }

        string targetFolderPath = Path.Combine(Directory.GetCurrentDirectory(), LUA_TARGET_FOLDER);

        // 1. 清理目标文件夹，确保干净的环境
        if (Directory.Exists(targetFolderPath))
        {
            Directory.Delete(targetFolderPath, true); // true 表示递归删除
        }
        Directory.CreateDirectory(targetFolderPath);
        AssetDatabase.Refresh(); // 刷新让 Unity 知道文件夹已创建

        // 2. 复制 .lua 文件为 .lua.txt 并收集新文件的路径
        string[] luaFiles = Directory.GetFiles(sourceFolderPath, "*.lua", SearchOption.AllDirectories);
        List<string> newAssetPaths = new List<string>();

        foreach (string sourceFilePath in luaFiles)
        {
            string fileName = Path.GetFileName(sourceFilePath); // 例如 "Main.lua"
            string destFilePath = Path.Combine(targetFolderPath, fileName + ".txt");
            File.Copy(sourceFilePath, destFilePath);
            
            // 将完整的系统路径转换为 Unity 的 "Assets/..." 路径
            newAssetPaths.Add(FullPathToAssetPath(destFilePath));
        }

        // 3. 再次刷新，让 Unity 识别所有新创建的 .txt 文件
        AssetDatabase.Refresh();

        Debug.Log($"Copied {luaFiles.Length} lua files to {LUA_TARGET_FOLDER}. Now setting them as addressable...");

        // 4. 将新文件添加到 Addressables
        SetAssetsAsAddressable(newAssetPaths, LUA_ADDRESSABLE_GROUP);
    }

    private static void SetAssetsAsAddressable(List<string> assetPaths, string groupName)
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("Addressable Asset Settings not found. Please initialize Addressables first.");
            return;
        }

        AddressableAssetGroup group = settings.FindGroup(groupName);
        if (group == null)
        {
            group = settings.CreateGroup(groupName, false, false, true, null);
        }

        foreach (string assetPath in assetPaths)
        {
            string guid = AssetDatabase.AssetPathToGUID(assetPath);
            if (string.IsNullOrEmpty(guid))
            {
                Debug.LogWarning($"Could not find GUID for asset at path: {assetPath}");
                continue;
            }

            AddressableAssetEntry entry = settings.CreateOrMoveEntry(guid, group);

            // 设置地址为 "文件名.lua"，例如 "Main.lua"
            string fileNameWithLuaExt = Path.GetFileNameWithoutExtension(assetPath); // "Main.lua"
            entry.address = fileNameWithLuaExt;
        }
        
        // 标记设置为已修改并保存
        EditorUtility.SetDirty(settings);
        AssetDatabase.SaveAssets();
        Debug.Log($"Successfully processed {assetPaths.Count} assets into Addressable group '{groupName}'.");
    }

    // 辅助函数：将完整物理路径转换为 "Assets/..." 格式
    private static string FullPathToAssetPath(string fullPath)
    {
        return "Assets" + fullPath.Substring(Application.dataPath.Length);
    }
}