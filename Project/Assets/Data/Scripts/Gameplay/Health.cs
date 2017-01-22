using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public static Health Instance {
        get;
        private set;
    }

    private void Awake() {
        Instance = this;
    }

    [SerializeField]
    private Animator myAnimator;

    private float speed = 1;

    public void Damage() {
        speed = 4.0f;
    }

    private void Update() {
        speed -= Time.deltaTime;
        if (speed < 1.0f)
            speed = 1.0f;
        myAnimator.SetFloat("speed", speed);
    }
}
