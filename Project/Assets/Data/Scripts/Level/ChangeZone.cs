using System.Collections;

using UnityEngine;

using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public sealed class ChangeZone : MonoBehaviour {

    [SerializeField]
    private Transform targetPoint;

    [SerializeField]
    private LevelZone levelToEnable;
    [SerializeField]
    private LevelZone levelToDisable;

    private void OnTriggerEnter(Collider other) {
        StartCoroutine(ChangeZoneCoroutine(other.transform));
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.layer = LayerDefinitions.PLAYER_TRIGGER_LAYER;
    }

    private IEnumerator ChangeZoneCoroutine(Transform playerTransform) {
        levelToDisable.SetActive(false);
        Movement playerMovement = playerTransform.GetComponent<Movement>();
        float speed = playerTransform.GetComponent<Movement>().Velocidad * 0.2f;
        float distance = (targetPoint.position - playerTransform.position).magnitude;
        //@TODO: Solo desactivar el input
        playerMovement.enabled = false;
        playerTransform.DOMove(targetPoint.position, distance / speed);
        yield return new WaitForSeconds(0.5f);
        if (levelToEnable != null) {
            levelToEnable.SetActive(true);
            playerMovement.enabled = true;
        } else {
            Debug.Log("LevelComplete");
        }
        gameObject.SetActive(false);
    }
}
