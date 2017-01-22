﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LevelZone : MonoBehaviour {

    [SerializeField]
    private EnemySpawn[] spawns;
    [SerializeField]
    private GameObject Nivel;

    private void Start()
    {
        //Nivel.activeSelf(false); ;

    }

    void Reset () {
        for (int i = 0; i < spawns.Length; ++i) {
            spawns[i].SetActive(false, true);
        }
    }

    public void SetActive(bool active) {
        for (int i = 0; i < spawns.Length; ++i) {
            spawns[i].SetActive(active, true);
        }
        Level.Instance.SetActiveLevelZone(this);
    }

    void desactivador() {
       // Nivel.activeSelf(); ;
       
    }
}
