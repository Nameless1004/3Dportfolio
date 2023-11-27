using RPG.Util;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EncryptJsonFiles : MonoBehaviour
{
    public string Key;

    public void EncryptFile()
    {
#if UNITY_EDITOR
        var test = Resources.LoadAll<TextAsset>("Data/");
        Dictionary<string, string> files = new Dictionary<string, string>();
        string at = "Assets/Resources";
        foreach (var t in test)
        {
            Debug.Log(t.name);
            string path = UnityEditor.AssetDatabase.GetAssetPath(t);
            int index = path.IndexOf(at);
            string newPath = path.Substring(index + at.Length + 1);
            newPath = newPath.Replace("Data", "EncryptedData");
            files.Add(newPath, JsonUtil.Encrypt(Key, t.text));
            Debug.Log(newPath);
        }
        if(AssetDatabase.IsValidFolder(Path.Combine(at, "EncryptedData")) == false)
        {
            AssetDatabase.CreateFolder(at, "EncryptedData");
        }

        foreach(var json in files)
        {
            var split = json.Key.Split('/');
            var segment = new ArraySegment<string>(split, 0, split.Length - 1);
            var fileName = split[split.Length - 1];
            Debug.Log(fileName);
            for(int i = 0; i < segment.Count; ++i)
            {
                string parentPath = Path.Combine(at, Path.Combine(segment.Slice(0, i).ToArray()));
                string combinePath = Path.Combine(at, Path.Combine(segment.Slice(0, i + 1).ToArray()));
                string folderName = Path.Combine(segment[i]);
                Debug.Log($"Parent : {parentPath} / {combinePath} / {name}");
                if (AssetDatabase.IsValidFolder(combinePath) == false)
                {
                    Debug.Log($"Folder Create : {parentPath + folderName}");
                    AssetDatabase.CreateFolder(parentPath, folderName);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }

            string filePath = Path.Combine(at, json.Key);
            if (ResourceCache.Load<TextAsset>(json.Key) == null)
            {
                Debug.Log($"FileCreate : {filePath}");
                File.Create(filePath).Close();                
            }
            Debug.Log(json.Value);
            File.WriteAllText(filePath, json.Value);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
    }

}
