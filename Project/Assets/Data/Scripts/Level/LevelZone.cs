using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LevelZone : MonoBehaviour {

    [SerializeField]
    private EnemySpawn[] spawns;

    // Use this for initialization
    private void Start () {
		
	}

    // Update is called once per frame
    private void Update () {
		
	}

    public void SetActive(bool active) {
        for (int i = 0; i < spawns.Length; ++i) {
            spawns[i].SetActive(active, true);
        }
    }
}
