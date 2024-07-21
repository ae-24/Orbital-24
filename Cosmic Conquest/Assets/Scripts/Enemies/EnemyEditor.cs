using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyState))]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyState enemyState = (EnemyState)target;
        if (GUILayout.Button("Generate Unique ID"))
        {
            enemyState.enemyID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(enemyState);
        }
    }
}
