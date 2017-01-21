using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {
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
    
    void Awake()
    {
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

}
