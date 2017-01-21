using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour {

    [SerializeField]
    private GameObject NuevaOnda;
    private Vector3 Direccion;
    [SerializeField]
    private float Speed;

    private Vector3 UltimaPosicion;

    // Use this for initialization
    void Start () {
    }

    public void Configure(Vector3 dir)
    {
        Direccion = dir;
    }

    // Update is called once per frame
    private void Update () {
        Collider[] Colliders;
        Collider[] ColliderPared;
        gameObject.transform.position += Direccion * Speed * Time.deltaTime;
        Destroy(gameObject, 4);

        Colliders = Physics.OverlapSphere(gameObject.transform.position, 1, LayerDefinitions.ENEMY_MASK);
        ColliderPared = Physics.OverlapSphere(gameObject.transform.position, 1, LayerDefinitions.INVISIBLE_OBSTACLE_LAYER);

        if (Colliders.Length > 0) {
            Destroy(gameObject);
        }

        else if (ColliderPared.Length > 0) {
            UltimaPosicion =  gameObject.transform.position;
            Destroy(gameObject);
            Instantiate(NuevaOnda);
        }
    }
}
