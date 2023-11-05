using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CustomEditor(typeof(EncryptJsonFiles))]
public class EncryptJsonFileButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EncryptJsonFiles file = (EncryptJsonFiles)target;
        if(GUILayout.Button("Encrypt"))
        {
            file.EncryptFile();
        }

    }
}
