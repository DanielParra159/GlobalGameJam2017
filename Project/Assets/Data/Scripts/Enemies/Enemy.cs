﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public sealed class Enemy : MonoBehaviour {

    private enum States {
        Walk,
        Attack,
        DoDamage,
        Die
    }

    [SerializeField]
    private EnemyIds enemyType;
    public EnemyIds EnemyType {
        get {
            return enemyType;
        }
    }
    [SerializeField]
    private EnemyAttacks enemyAttack;
    public EnemyAttacks EnemyAttack {
        get {
            return enemyAttack;
        }
    }

    private Transform myTransform;
    public Transform MyTransform {
        get {
            return myTransform;
        }
    }

    private GameObject myGameObject;
    public GameObject MyGameObject {
        get {
            return myGameObject;
        }
    }

    private Rigidbody myRigidbody;
    private SpriteRenderer myRenderer;
    private NavMeshAgent myNavMeshAgent;

    private EnemySpawn spawn;

    private States currentState;
    private States CurrentState {
        get {
            return currentState;
        }
        set {
            switch (value) {
                case States.Walk:
                    myNavMeshAgent.destination = Movement.Instance.transform.position;
                    break;
                case States.Attack:
                    //Trigger anim
                    //myNavMeshAgent.destination = MyTransform.position;
                    myNavMeshAgent.Stop();
                    break;
                case States.Die:
                    break;
            }
            currentState = value;
        }
    }

    [SerializeField]
    private int health = 5;
    [SerializeField]
    private float minDistanceToAttack = 1.0f;
    [SerializeField]
    private float attackDistance = 1.0f;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private Color colorDamage = Color.red;

    // Use this for initialization
    private void Awake () {
        myGameObject = gameObject;
        myTransform = gameObject.transform;
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        myNavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update () {
        switch (currentState) {
            case States.Walk:
                myNavMeshAgent.destination = Movement.Instance.transform.position;
                if ((myNavMeshAgent.destination - MyTransform.position).sqrMagnitude < minDistanceToAttack) {
                    //Atacamos
                    CurrentState = States.Attack;
                }
                break;
            case States.Attack:
                break;
        }
    }

    public void Reset(EnemySpawn spawn) {
        this.spawn = spawn;
        CurrentState = States.Walk;
    }

    //Se llama desde el animator
    public void Attack() {
        Movement.Instance.DoDamage(damage);
    }

    public void DoDamage(int damage, Vector3 damageDir) {
        CurrentState = States.DoDamage;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Insert(0.0f, myRenderer.DOColor(colorDamage, 0.2f));
        mySequence.Insert(0.2f, myRenderer.DOColor(Color.white, 0.2f));

        myRigidbody.AddForce(damageDir);
    }


    private void Die() {
        spawn.EnemyDead(this);
    }

}
