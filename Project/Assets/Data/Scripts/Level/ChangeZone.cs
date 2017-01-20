using System.Collections;

using UnityEngine;

using DG.Tweening;

public sealed class ChangeZone : MonoBehaviour {

    [SerializeField]
    private Transform targetPoint;

    [SerializeField]
    private LevelZone levelToEnable;
    [SerializeField]
    private LevelZone levelToDisable;

    private void OnTriggerEnter(Collider other) {
        StartCoroutine(ChangeZoneCoroutine(other.transform));
        
    }

    private IEnumerator ChangeZoneCoroutine(Transform playerTransform) {
        levelToDisable.SetActive(false);
        float speed = playerTransform.GetComponent<Movement>().Velocidad;
        //targetPoint.
        playerTransform.DOMove(targetPoint.position, 0.5f);
        yield return new WaitForSeconds(0.5f);
        levelToEnable.SetActive(true);
    }
}
