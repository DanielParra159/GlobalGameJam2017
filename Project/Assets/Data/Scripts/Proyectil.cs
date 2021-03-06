﻿using UnityEngine;

using Common.Utils;

using DG.Tweening;

public class Proyectil : MonoBehaviour
{

    [SerializeField]
    private GameObject trailGO;

    [SerializeField]
    private GameObject NuevaOnda;
    private Vector3 Direccion;
    [SerializeField]
    private float Speed;

    private float nextTrailTime = 0;
    private float delayBetweenTrails = 0.1f;

    private Transform UltimaPosicion;

    // Use this for initialization
    void Start()
    {
        trailGO.CreatePool(300);
        Destroy(gameObject, 4);

    }

    public void Configure(Vector3 dir)
    {
        Direccion = dir;
        float angle = Mathf.Atan2(Direccion.x, Direccion.z) * Mathf.Rad2Deg;
        transform.Rotate(0, angle, 0);
        nextTrailTime = Time.time + delayBetweenTrails;
    }

    // Update is called once per frame
    private void Update()
    {
        gameObject.transform.position += Direccion * Speed * Time.deltaTime;

        Collider[] colision = Physics.OverlapSphere(gameObject.transform.position, 0.5f,LayerDefinitions.ENEMY_MASK);
        for (int i = 0; i < colision.Length; i++) {
            Enemy enemigo = colision[i].GetComponent<Enemy>();
            if (enemigo != null) { 
            enemigo.DoDamage(1,Direccion);
            }
        }

        if (Time.time > nextTrailTime)
        {
            SpriteRenderer spriteRenderer = trailGO.SpawnPool(transform.position, transform.rotation).GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.DOFade(1.0f, 0.0f);
            spriteRenderer.DOFade(0.0f, 0.5f);
            nextTrailTime = Time.time + delayBetweenTrails;
        }
        if (colision.Length > 0) {
            Destroy(gameObject);

        }

    }

    
    

     private void nuevaonda(ContactPoint posicion) {
            for (int i = 0; i < 2; i++)
            {

               GameObject instanciado = Instantiate(NuevaOnda, posicion.point, Quaternion.identity);
                instanciado.GetComponent<Proyectil>().Configure(Direccion);

        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerDefinitions.ENEMY_LAYER) {
            Destroy(gameObject);
        } else if (collision.gameObject.layer == LayerDefinitions.ENEMY_PROJECTILE_LAYER) {
            collision.gameObject.GetComponent<Dentadura>().Destruir();
            Destroy(gameObject);
        } else if (collision.gameObject.layer == LayerDefinitions.WALL_LAYER) {
            Destroy(gameObject);

            ContactPoint contact = collision.contacts[0];

            Direccion = 2 * (Vector3.Dot(Direccion, Vector3.Normalize(contact.normal))) * Vector3.Normalize(contact.normal) - Direccion; //Following formula  v' = 2 * (v . n) * n - v

            Direccion *= -1; //Had to multiply everything by -1. Don't know why, but it was all backwards.
            nuevaonda(contact);

        }


    }

}