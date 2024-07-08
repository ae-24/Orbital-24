using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Collectible))]
public class CollectibleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Collectible collectible = (Collectible)target;
        if (GUILayout.Button("Generate Unique ID"))
        {
            collectible.collectibleID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(collectible);
        }
    }
}
