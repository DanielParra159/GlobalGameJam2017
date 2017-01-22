using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigojefe1 : MonoBehaviour {

    private bool ataque1, ataque2;
    [SerializeField]
    private GameObject EnemySpawnRight, EnemySpawnLeft;
    private Animator myAnimator;


    // Use this for initialization
    void Start () {
        Combate();
        myAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void Combate() {
        new WaitForSeconds(3);
       // megaataque1();

    }
    /*
    void megaataque1() {
        myAnimator.SetBool("ataque1", true);
        GameObject spawnr = Instantiate(EnemySpawnRight);
        GameObject spawni = Instantiate(EnemySpawnLeft);
        Destroy(spawnr, 1);
        Destroy(spawni, 1);
    }
    void megaataque2() {

    }*/
}
