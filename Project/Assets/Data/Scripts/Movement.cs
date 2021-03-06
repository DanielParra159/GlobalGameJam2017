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

    private Vector3 smoothPosition;
    public Vector3 SmoothPosition {
        get {
            return smoothPosition;
        }
    }

    int lastDirZ = 0;
    int lastDirX = 0;
    bool moving = false;

    private Vector3 direction;
    private Vector3 movingDirection;

    void Awake()
    {
        Instance = this;
        rb = gameObject.GetComponent<Rigidbody>();
    } 

    // Use this for initialization
    void Start () {
        Reset();
    }

    private void OnDisable() {
        smoothPosition = transform.position;
    }

    public void Reset() {
        smoothPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (direction.sqrMagnitude > 0) {
            moving = true;
            movingDirection = direction;
        } else {
            moving = false;
            direction = movingDirection;
        }

        if (Input.GetButton("Fire")) {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.y = 0.1f;

            direction = Vector3.Normalize((position - transform.position));

            myAnimator.SetBool("Attack", true);
        } else {
            myAnimator.SetBool("Attack", false);
        }
        SetSpriteDir(direction.x, direction.z);

        /*if (Input.GetButton("Pause")) {
            PauseCanvas.Instance.gameObject.SetActive(true);
        }*/
    }

    void FixedUpdate()
    {
        if (moving)
            rb.MovePosition(rb.position + movingDirection * velocidad *  Time.fixedDeltaTime);
        smoothPosition = Vector3.Lerp(smoothPosition, rb.position, (rb.position-smoothPosition).magnitude * 20.0f* Time.fixedDeltaTime);
    }

    public void DoDamage(int damage, Vector3 damageDir) {
        damageDir.y = 0.0f;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Insert(0.0f, myRenderer.DOColor(colorDamage, 0.2f));
        mySequence.Insert(0.2f, myRenderer.DOColor(Color.white, 0.2f));
        myRenderer.transform.DOShakePosition(0.2f, 0.01f, 10, 90, false, false);

        Health.Instance.Damage();
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

        if (auxDirX != lastDirX) {
            myAnimator.SetInteger("DirX", auxDirX);
            lastDirX = auxDirX;
        }
        if (auxDirZ != lastDirZ) {
            myAnimator.SetInteger("DirZ", auxDirZ);
            lastDirZ = auxDirZ;
        }
    }

}
