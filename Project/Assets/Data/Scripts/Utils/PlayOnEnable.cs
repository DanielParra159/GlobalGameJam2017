using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnEnable : MonoBehaviour {

    [SerializeField]
    private AudioSource audio;

    private void OnEnable() {
        audio.Play();
    }
}
