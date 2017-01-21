using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Common.Utils;

public sealed class EnemyManager : MonoBehaviour {
    public static EnemyManager Instance {
        get;
        private set;
    }

    [SerializeField]
    private GameObject grandmotherPrefab;

    private Dictionary<EnemyIds, GameObject> enemyPrefabs = new Dictionary<EnemyIds, GameObject>();

    // Use this for initialization
    private void Awake () {
        Instance = this;

        grandmotherPrefab.CreatePool(20);
        enemyPrefabs.Add(EnemyIds.Grandmother, grandmotherPrefab);
    }

    // Update is called once per frame
    private void Update () {
		
	}

    public Enemy SpawnEnemy(EnemyIds enemyType, Vector3 position, EnemySpawn spawn) {
        GameObject enemyGO = enemyPrefabs[enemyType].SpawnPool(position);
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.Reset(spawn);

        return enemy;
    }
}
