using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Enemy : MonoBehaviour {

    [SerializeField]
    private EnemyTypes enemyType;
    public EnemyTypes EnemyType {
        get {
            return enemyType;
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

    private EnemySpawn spawn;

    // Use this for initialization
    private void Awake () {
        myGameObject = gameObject;
        myTransform = gameObject.transform;
    }

    // Update is called once per frame
    private void Update () {
		
	}

    public void Reset(EnemySpawn spawn) {
        this.spawn = spawn;
    }

    public void DoDamage() {

    }

    private void Die() {
        spawn.EnemyDead(this);
    }

}
