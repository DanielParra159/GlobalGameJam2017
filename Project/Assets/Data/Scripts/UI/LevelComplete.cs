using Common.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {

    [SerializeField]
    private AudioSource select;

    public void Menu() {
        SceneManager.LoadScene("Game");
        ExtensionPool.ClearAllPool();
        select.Play();
    }
}
