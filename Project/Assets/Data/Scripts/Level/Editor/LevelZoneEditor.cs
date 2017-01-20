using UnityEditor;
using UnityEngine;

using Common.Utils;

[CustomEditor(typeof(LevelZone), true)]
public sealed class LevelZoneEditor : CommonEditor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        SerializedProperty spawns = serializedObject.FindProperty("spawns");

        if (GUILayout.Button("Build")) {
            LevelZone levelZone = serializedObject.targetObject as LevelZone;
            EnemySpawn[] enemySpawns = levelZone.GetComponentsInChildren<EnemySpawn>();
            spawns.arraySize = enemySpawns.Length;
            for (int i = 0; i < spawns.arraySize; ++i) {
                spawns.GetArrayElementAtIndex(i).objectReferenceValue = enemySpawns[i];
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    protected override void OnEnable() {
        callBase = true;
        base.OnEnable();
    }
}
