using UnityEngine;

using Common.Utils;

using DG.Tweening;

public class Proyectil : MonoBehaviour {

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
    void Start () {
        trailGO.CreatePool(300);
        Destroy(gameObject, 4);

    }

    public void Configure(Vector3 dir)
    {
        Direccion = dir;
        float angle = Mathf.Atan2(Direccion.x, Direccion.z) * Mathf.Rad2Deg;
        transform.Rotate(0, angle, 0);
        nextTrailTime = Time.time + delayBetweenTrails;
        Debug.Log(gameObject.transform.position);
    }

    // Update is called once per frame
    private void Update () {
        Collider[] Colliders;
        Collider[] ColliderPared;
        gameObject.transform.position += Direccion * Speed * Time.deltaTime;

        Colliders = Physics.OverlapSphere(gameObject.transform.position, 1, LayerDefinitions.ENEMY_MASK);
        ColliderPared = Physics.OverlapSphere(gameObject.transform.position, 1, LayerDefinitions.WALL_MASK);

        if (Colliders.Length > 0) {
            Destroy(gameObject);
        }
        else if (ColliderPared.Length > 0) {
            UltimaPosicion = gameObject.transform;
            Destroy(gameObject);
            nuevaonda(UltimaPosicion);
                        
        

        } else if (Time.time > nextTrailTime)
        {
            SpriteRenderer spriteRenderer = trailGO.SpawnPool(transform.position, transform.rotation).GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.DOFade(1.0f, 0.0f);
            spriteRenderer.DOFade(0.0f, 0.5f);
            nextTrailTime = Time.time + delayBetweenTrails;
        }
    }

    private void nuevaonda(Transform posicion) {
        for (int i = 0; i < 2; i++)
        {

            Instantiate(NuevaOnda, posicion.transform.position, Quaternion.Euler(45, 0, 0));
            gameObject.transform.position += Direccion * Speed * Time.deltaTime;

        }


    }
}
