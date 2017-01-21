using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{

    private float BulletFireRate = 0.05f;
    [SerializeField]
    private GameObject ProjectilePrefab;
    private GameObject Personaje;
    public int NumberOfBullets = 2;
    private float ProjectileOffset = 1.3f;
    private Vector3 Direccion;

    private void Awake()
    {
        Personaje = gameObject.gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown("mouse 0"))
            StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        Vector3 Position = Vector3.zero;

        for (int i = 0; i < NumberOfBullets; i++)
        {

            Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Position.y = 0.1f;

            Direccion = Vector3.Normalize((Position - Personaje.transform.position));

            GameObject pro = Instantiate(ProjectilePrefab,
                        Position,
                        Quaternion.identity);
            pro.GetComponent<Proyectil>().Configure(Direccion);
            yield return new WaitForSeconds(BulletFireRate);
        }

    }
}