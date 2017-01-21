using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour {

    [SerializeField]
    private GameObject Personaje;
    private Vector3 Direccion;

    private float Speed;

	// Use this for initialization
	void Start () {
        Direccion = Vector3.Normalize((gameObject.transform.position - Personaje.transform.position));
        Speed = 3;
    }

    // Update is called once per frame
    void Update () {
        transform.position += Direccion * Speed * Time.deltaTime;
    }
}
