using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour {

    [SerializeField]
    private GameObject Personaje;
    private Vector3 Direccion;
    private Vector3 Personajeposicion;
    [SerializeField]
    private float Speed;

	// Use this for initialization
	void Start () {
    }

    public void Configure(Vector3 dir)
    {
        Direccion = dir;
    }

    // Update is called once per frame
    void Update () {
        gameObject.transform.position += Direccion * Speed * Time.deltaTime;

        Destroy(gameObject, 4);
    }
}
