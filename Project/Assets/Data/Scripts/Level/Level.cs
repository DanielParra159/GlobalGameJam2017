using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public sealed class Level : MonoBehaviour {
    public static Level Instance {
        get;
        private set;
    }

    [SerializeField]
    private LevelZone initialLevelZone;
    private LevelZone currentLevelZone;

    [SerializeField]
    private Transform playerSpawn;

    // Use this for initialization
    private void Awake () {
        Instance = this;
    }

    private void Start() {
        initialLevelZone.SetActive(true);
    }

    public void Reset() {
        initialLevelZone.SetActive(true);
        Movement.Instance.transform.position = playerSpawn.position;
        MainCamara.Instance.ForceMoveTo(playerSpawn.position);
    }

    public void SetActiveLevelZone(LevelZone levelZone) {
        currentLevelZone = levelZone;
    }
}
