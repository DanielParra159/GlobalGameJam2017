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

    [SerializeField]
    private AudioSource gameMusic;
    [SerializeField]
    private AudioSource gameMusicDura;

    // Use this for initialization
    private void Awake () {
        Instance = this;
    }

    private void Start() {
        initialLevelZone.SetActive(true);
    }

    public void Reset() {
        initialLevelZone.SetActive(true);
        gameMusicDura.Stop();
        Movement.Instance.transform.position = playerSpawn.position;
        MainCamara.Instance.ForceMoveTo(playerSpawn.position);
    }

    public void SetActiveLevelZone(LevelZone levelZone) {
        currentLevelZone = levelZone;
        if (levelZone != initialLevelZone) {
            gameMusicDura.Play();
            gameMusic.DOFade(0.0f, 0.3f);
            if (gameMusicDura.isPlaying) {
                gameMusicDura.DOFade(0.0f, 0.0f);
                gameMusicDura.DOFade(1.0f, 0.3f);
            }
        } else {
            gameMusicDura.Play();
            gameMusic.DOFade(1.0f, 0.3f);
            gameMusicDura.DOFade(0.0f, 0.0f);
            gameMusicDura.Stop();
        }
    }
}
