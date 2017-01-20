using System.Collections.Generic;

using UnityEngine;

using Common.Utils;

public sealed class EnemySpawn : MonoBehaviour {

    [SerializeField]
    private EnemyTypes[] enemiesToSpawn;
    [SerializeField]
    private int[] totalEnemies;
    private int[] numEnemiesSpawned;
    [SerializeField]
    private float[] timeBetweenSpawns;
    [SerializeField]
    private float[] delayToStartSpawn;

    private float[] nextSpawnTime;

    private List<Enemy> enemiesSpawned = new List<Enemy>();

    [SerializeField]
    private float spawnRadius = 5.0f;

    private void Awake() {
#if UNITY_EDITOR
        if (enemiesToSpawn.Length != totalEnemies.Length || enemiesToSpawn.Length != timeBetweenSpawns.Length
            || enemiesToSpawn.Length != delayToStartSpawn.Length)
            Debug.LogError("enemiesToSpawn o totalEnemies o timeBetweenSpawns no tienen el mismo tamaño " + gameObject.name);
#endif
        nextSpawnTime = new float[timeBetweenSpawns.Length];
        numEnemiesSpawned = new int[timeBetweenSpawns.Length];
    }

    // Use this for initialization
    private void Start () {
        gameObject.SetActive(false);
	}

    // Update is called once per frame
    private void Update () {
        for (int i = 0; i < timeBetweenSpawns.Length; ++i) {
            if (Time.time > timeBetweenSpawns[i] && numEnemiesSpawned[i] < totalEnemies[i]) {
                nextSpawnTime[i] = Time.time + timeBetweenSpawns[i];
                ++numEnemiesSpawned[i];

                Vector3 position = Random.insideUnitSphere;
                position.x *= Random.Range(0.0f, spawnRadius);
                position.y = 0.1f;
                position.z *= Random.Range(0.0f, spawnRadius);

                Enemy enemy = EnemyManager.Instance.SpawnEnemy(enemiesToSpawn[i], position, this);
                enemiesSpawned.Add(enemy);
            }
        }
	}

    public void SetActive(bool active, bool killSpawnedEnemies) {
        gameObject.SetActive(active);
        if (active) {
            for (int i = 0; i < timeBetweenSpawns.Length; ++i) {
                nextSpawnTime[i] = Time.time + delayToStartSpawn[i];
            }
        }
        if (killSpawnedEnemies) {
            for (int i = 0; i < enemiesSpawned.Count; ++i) {
                enemiesSpawned[i].MyGameObject.RecyclePool();
            }
            enemiesSpawned.Clear();
        }
    }

    public void EnemyDead(Enemy enemy) {
        enemiesSpawned.Remove(enemy);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
#endif
}
