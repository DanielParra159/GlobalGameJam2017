using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public sealed class MainCamara : MonoBehaviour {
    public static MainCamara Instance {
        get;
        private set;
    }

    [SerializeField]
    private Vector3 offset = new Vector3(0.0f, 5.0f, 0.0f);

    private float offsetZ = 0.0f;

    private bool followPlayer = true;
    public bool FollowPlayer {
        get {
            return followPlayer;
        }
        set {
            followPlayer = value;
        }
    }

    private Tweener shake;

    /*[SerializeField]
    private float speed = 20.0f;*/

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        //offset = new Vector3(0,5,0);
    }

    //public float smoothTime = 1.5f;
    //private Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void LateUpdate () {
        transform.GetChild(0).position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + offsetZ);

        //Vector3 playerPosition = Movement.Instance.SmoothPosition + offset;
        if (followPlayer) {
            Vector3 playerPosition = Movement.Instance.SmoothPosition + offset;
            gameObject.transform.position = playerPosition;
        }
        //camara.transform.position = Vector3.Lerp(camara.transform.position, playerPosition, (playerPosition - camara.transform.position).magnitude * 0.5f * speed * Time.deltaTime);
        //if ((playerPosition - camara.transform.position).sqrMagnitude > 0.1f)
        //    camara.transform.position += (playerPosition - camara.transform.position).normalized * (playerPosition - camara.transform.position).magnitude* speed * Time.deltaTime;
        //transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, smoothTime);
        //camara.transform.DOMove(playerPosition + offset, 0.2f);
        //camara.transform.DOMove(playerPosition + offset, 10/(playerPosition - camara.transform.position).magnitude);
    }

    public void MoveTo(Vector3 position, float time, bool applyOffset = true) {
        if (applyOffset)
            position += offset;
        MainCamara.Instance.FollowPlayer = false;
        transform.DOMove(position, time).OnComplete(()=> {
            FollowPlayer = true;
        });
    }

    public void ForceMoveTo(Vector3 position, bool applyOffset = true) {
        if (applyOffset)
            position += offset;
        transform.position = position;
        MainCamara.Instance.FollowPlayer = false;
    }

    public void Shake(float duration) {
        if (shake != null) {
            shake.Kill(false);
            transform.GetChild(0).position = transform.position;
        }
        shake = transform.GetChild(0).DOShakePosition(duration, 1, 10, 90f);
    }

    public void BossCamera(bool enable) {
        if (enable) {
            transform.GetChild(0).GetComponent<Camera>().DOOrthoSize(6.0f, 1.0f);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Insert(0.0f, DOTween.To(() => offsetZ, x => offsetZ = x, 0.0f, 2.2f));
        } else {
            transform.GetChild(0).GetComponent<Camera>().DOOrthoSize(4.0f, 1.0f);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Insert(0.0f, DOTween.To(() => offsetZ, x => offsetZ = x, 2.2f, 0.0f));
        }
    }
}
