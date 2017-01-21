using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;
using Common.Utils;

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
    [SerializeField]
    private SpriteRenderer myRenderer;
    private NavMeshAgent myNavMeshAgent;

    private EnemySpawn spawn;

    [SerializeField]
    private GameObject explosionPrefab;

    private States currentState;
    private States CurrentState {
        get {
            return currentState;
        }
        set {
            switch (value) {
                case States.Walk:
                    myNavMeshAgent.destination = Movement.Instance.transform.position;
                    myNavMeshAgent.Resume();
                    SetSpriteDir(transform.forward.x, transform.forward.z);
                    break;
                case States.Attack:
                    //Trigger anim
                    //myNavMeshAgent.destination = MyTransform.position;
                    myAnimator.SetBool("Attack", true);
                    Attack();
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
    private int currentHealth = 5;
    [SerializeField]
    private float minDistanceToAttack = 0.5f;
    [SerializeField]
    private float attackDistance = 0.5f;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private Color colorDamage = Color.red;

    [SerializeField]
    private Animator myAnimator;

    int lastDirZ = 0;
    int lastDirX = 0;
    bool moving = false;

    // Use this for initialization
    private void Awake () {
        explosionPrefab.CreatePool(50);
        myGameObject = gameObject;
        myTransform = gameObject.transform;
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        myNavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        myAnimator = myRenderer.GetComponent<Animator>();

        myRenderer.transform.SetParent(null);
    }

    private void OnDisable() {
        myRenderer.gameObject.SetActive(false);
    }
    private void OnEnable() {
        myRenderer.gameObject.SetActive(true);
    }

    // Update is called once per frame
    private void Update () {
        switch (currentState) {
            case States.Walk:
                moving = true;
                myNavMeshAgent.destination = Movement.Instance.transform.position;
                SetSpriteDir(transform.forward.x, transform.forward.z);
                if ((myNavMeshAgent.destination - MyTransform.position).sqrMagnitude < minDistanceToAttack) {
                    //Atacamos
                    CurrentState = States.Attack;
                }
                break;
            case States.Attack:
                moving = false;
                break;
        }
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

    public void Reset(EnemySpawn spawn) {
        this.spawn = spawn;
        CurrentState = States.Walk;
        currentHealth = health;
    }

    //Se llama desde el animator
    public void Attack() {
        Movement.Instance.DoDamage(damage, Vector3.Normalize(myNavMeshAgent.destination - MyTransform.position));
        StartCoroutine(Temp());
    }
    private IEnumerator Temp() {
        yield return new WaitForSeconds(1.0f);
        Walk();
    }

    //Se llama desde el animator
    public void Walk() {
        myAnimator.SetBool("Attack", false);
        CurrentState = States.Walk;
    }

    public void DoDamage(int damage, Vector3 damageDir) {
        damageDir.y = 0.0f;
        CurrentState = States.DoDamage;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Insert(0.0f, myRenderer.DOColor(colorDamage, 0.2f));
        mySequence.Insert(0.2f, myRenderer.DOColor(Color.white, 0.2f));
        myRenderer.transform.DOShakePosition(0.2f, 0.01f, 10, 90, false, false);

        myRigidbody.AddForce(damageDir);

        currentHealth -= damage;
        if (currentHealth <= 0) {
            Die();
        }
    }


    private void Die() {
        MainCamara.Instance.Shake(1.0f);
        spawn.EnemyDead(this);
        explosionPrefab.SpawnPool(transform.position);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        if (Movement.Instance != null)
            Gizmos.DrawLine(transform.position, Vector3.Normalize(Movement.Instance.transform.position - transform.position) * minDistanceToAttack);
    }
#endif

}
