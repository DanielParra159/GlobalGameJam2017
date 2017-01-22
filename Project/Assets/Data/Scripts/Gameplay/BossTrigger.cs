using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject[] collidersToEnable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        for (int i = 0; i < collidersToEnable.Length; ++i) {
            collidersToEnable[i].SetActive(true);

            MainCamara.Instance.BossCamera(true);
        }
    }
}
