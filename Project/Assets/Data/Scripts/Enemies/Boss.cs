using Common.Utils;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Boss : Enemy {

    [SerializeField]
    private AudioSource die;
    [SerializeField]
    private AudioSource damge;
    [SerializeField]
    private AudioSource attack1;
    [SerializeField]
    private AudioSource attack2;

    [SerializeField]
    private AudioSource bossMusic;
    [SerializeField]
    private AudioSource[] gameMusics;

    [SerializeField]
    private GameObject[] disableOnDead;

    [SerializeField]
    private EnemySpawn[] spawns;

    [SerializeField]
    private float timeBetweenAttacks = 5.0f;
    private float nextAttack = 0.0f;

    [SerializeField]
    private GameObject dentadura;

    protected override void Awake() {
        removeParent = false;
        base.Awake();

        dentadura.CreatePool(5);
    }

    private void OnEnable() {
        currentHealth = health;

        for (int i = 0; i < gameMusics.Length; ++i) {
            gameMusics[i].DOFade(0.0f, 1.0f);
        }

        StartCoroutine(retrasoMusica());

        nextAttack = Time.time + timeBetweenAttacks;

    }

    private IEnumerator retrasoMusica() {
        yield return new WaitForSeconds(1.0f);

        bossMusic.Play();
        bossMusic.DOFade(1.0f, 1.0f);
    }

    private void Update() {

        if (Time.time > nextAttack) {
            if (currentHealth < health * 0.5f) {
                if (Random.Range(0.0f, 1.0f) < 0.5f) {
                    myAnimator.SetTrigger("Attack2");
                } else {
                    myAnimator.SetTrigger("Attack1");
                }
            } else {
                myAnimator.SetTrigger("Attack1");
            }
            nextAttack = Time.time + timeBetweenAttacks;
        }
    }

    public override void DoDamage(int damage, Vector3 damageDir) {
        /*if (Vector3.Dot(damageDir, new Vector3(0, 0 - 1)) > 0) {
            return;
        }
            Debug.Log(Vector3.Dot(damageDir, new Vector3(0, 0 - 1)));*/
        currentHealth -= damage;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Insert(0.0f, myRenderer.DOColor(Color.red, 0.2f));
        mySequence.Insert(0.2f, myRenderer.DOColor(Color.white, 0.2f));
        if (currentHealth <= 0) {
            Die();
        } else {
            damge.Play();
        }
    }

    private void Die() {
        MainCamara.Instance.Shake(1.0f);
        explosionPrefab.SpawnPool(transform.position);

        die.Play();

        for (int i = 0; i < disableOnDead.Length; ++i) {
            disableOnDead[i].SetActive(false);
        }

        gameObject.SetActive(false);
    }

    public void Attack1() {
        for (int i = 0; i < spawns.Length; ++i) {
            spawns[i].SetActive(true, false);
        }
        attack1.Play();
    }

    public void Attack2() {
        dentadura.SpawnPool(MyTransform.position);
        attack2.Play();
    }
}
