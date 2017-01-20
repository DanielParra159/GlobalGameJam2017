using UnityEditor;

using Common.Utils;

[CustomEditor(typeof(EnemySpawn), true)]
public sealed class EnemySpawnEditor : CommonEditor {


    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        SerializedProperty enemiesToSpawn = serializedObject.FindProperty("enemiesToSpawn");
        SerializedProperty totalEnemies = serializedObject.FindProperty("totalEnemies");
        SerializedProperty timeBetweenSpawns = serializedObject.FindProperty("timeBetweenSpawns");
        SerializedProperty delayToStartSpawn = serializedObject.FindProperty("delayToStartSpawn");

        if (totalEnemies.arraySize != enemiesToSpawn.arraySize || timeBetweenSpawns.arraySize != enemiesToSpawn.arraySize
            || delayToStartSpawn.arraySize != enemiesToSpawn.arraySize) {
            totalEnemies.arraySize = enemiesToSpawn.arraySize;
            timeBetweenSpawns.arraySize = enemiesToSpawn.arraySize;
            delayToStartSpawn.arraySize = enemiesToSpawn.arraySize;
        }

        serializedObject.ApplyModifiedProperties();
    }

    protected override void OnEnable() {
        callBase = true;
        base.OnEnable();
    }
}
