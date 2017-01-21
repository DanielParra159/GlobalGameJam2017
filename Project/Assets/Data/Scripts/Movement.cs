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
    private BoxCollider Cajita;

    public float Velocidad
    {
        get
        {
            return velocidad;
        }
    }
    

    Rigidbody rb;
    
    void Awake()
    {
        Instance = this;
        rb = gameObject.GetComponent<Rigidbody>();
        Cajita = gameObject.GetComponent<BoxCollider>();
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

    public void DoDamage(int damage) {

    }

}
