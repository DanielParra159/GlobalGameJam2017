using Common.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dentadura : MonoBehaviour {

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private float speed = 5;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += (Movement.Instance.transform.position - transform.position).normalized * Time.deltaTime * speed;
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerDefinitions.PLAYER_LAYER) {
            gameObject.transform.parent.gameObject.RecyclePool();
        } else if (other.gameObject.layer == LayerDefinitions.PLAYER_PROJECTILE_LAYER) {
            explosion.CreatePool(50);
            gameObject.transform.parent.gameObject.RecyclePool();
        }
    }

    public void Destruir() {
        gameObject.transform.parent.gameObject.RecyclePool();
    }
}
