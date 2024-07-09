using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyMovement))]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyMovement enemyMovement = (EnemyMovement)target;
        if (GUILayout.Button("Generate Unique ID"))
        {
            enemyMovement.enemyID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(enemyMovement);
        }
    }
}
