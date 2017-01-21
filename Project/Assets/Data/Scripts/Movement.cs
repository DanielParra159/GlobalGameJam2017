﻿using System.Collections;
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
    private Animator myAnimator;
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
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        SetSpriteDir(direction.x, direction.z);

        rb.MovePosition(rb.position + direction * velocidad *  Time.fixedDeltaTime);
        
    }

    public void DoDamage(int damage, Vector3 damageDir) {
        damageDir.y = 0.0f;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Insert(0.0f, myRenderer.DOColor(colorDamage, 0.2f));
        mySequence.Insert(0.2f, myRenderer.DOColor(Color.white, 0.2f));
        myRenderer.transform.DOShakePosition(0.2f, 0.01f, 10, 90, false, false);
    }

    public void SetSpriteDir(float dirX, float dirZ) {
        int auxDirX = 0;
        if (dirX > 0.0f)
            auxDirX = 1;
        else if (dirX < 0.0f)
            auxDirX = -1;

        int auxDirZ = 0;
        if (dirZ > 0.0f)
            auxDirZ = -1;
        else if (dirZ < 0.0f)
            auxDirZ = 1;
        myAnimator.SetInteger("DirX", auxDirX);
        myAnimator.SetInteger("DirZ", auxDirZ);
    }

}
