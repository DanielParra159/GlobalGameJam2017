using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Camara : MonoBehaviour {
    [SerializeField]
    private GameObject Personaje;

    private GameObject camara;

    Vector3 offset;

	// Use this for initialization
	void Start () {
        camara = gameObject.gameObject;
        offset = new Vector3(0,5,0);
    }
	
	// Update is called once per frame
	void Update () {

        camara.transform.DOMove(Personaje.transform.position + offset,3);


    }
}
