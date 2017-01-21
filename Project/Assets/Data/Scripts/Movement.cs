using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(Rigidbody))]
public sealed class Movement : MonoBehaviour {
    public static Movement Instance {
        get;
        private set;
    }
    [SerializeField]
    private float velocidad;

    public float Velocidad
    {
        get
        {
            return velocidad;
        }
    }
    

    Rigidbody rb;
    [SerializeField]
    private SpriteRenderer myRenderer;
    [SerializeField]
    private Color colorDamage = Color.red;

    void Awake()
    {
        Instance = this;
        rb = gameObject.GetComponent<Rigidbody>();
    } 

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update()
    {



    }

    void FixedUpdate()
    {
        Vector3 direccionH = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));

        rb.MovePosition(rb.position + direccionH * velocidad *  Time.fixedDeltaTime);
        
    }

    public void DoDamage(int damage, Vector3 damageDir) {
        damageDir.y = 0.0f;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Insert(0.0f, myRenderer.DOColor(colorDamage, 0.2f));
        mySequence.Insert(0.2f, myRenderer.DOColor(Color.white, 0.2f));
        myRenderer.transform.DOShakePosition(0.2f, 0.01f, 10, 90, false, false);
    }

}
