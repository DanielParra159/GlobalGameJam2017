using System.Collections;

using UnityEngine;

using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public sealed class ChangeZone : MonoBehaviour {

    [SerializeField]
    private Transform targetPoint;

    [SerializeField]
    private LevelZone levelToDisable;
    [SerializeField]
    private GameObject[] objectsToDisable;
    [SerializeField]
    private LevelZone levelToEnable;
    [SerializeField]
    private GameObject[] objectsToEnable;

    private void OnTriggerEnter(Collider other) {
        StartCoroutine(ChangeZoneCoroutine(other.transform));
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.layer = LayerDefinitions.PLAYER_TRIGGER_LAYER;
    }

    private IEnumerator ChangeZoneCoroutine(Transform playerTransform) {
        levelToDisable.SetActive(false);
        for (int i = 0; i < objectsToDisable.Length; ++i) {
            objectsToDisable[i].SetActive(false);
        }
        Movement playerMovement = playerTransform.GetComponent<Movement>();
        float speed = Movement.Instance.Velocidad * 0.2f;
        Vector3 targetPosition = targetPoint.position;
        targetPosition.y = playerMovement.transform.position.y;
        float distance = (targetPosition - playerTransform.position).magnitude;
        //@TODO: Solo desactivar el input
        playerMovement.enabled = false;
        playerTransform.DOMove(targetPosition, distance / speed);
        yield return new WaitForSeconds(distance / speed);
        if (levelToEnable != null) {
            MainCamara.Instance.MoveTo(targetPosition, (distance / speed)*0.5f);
            yield return new WaitForSeconds((distance / speed) * 0.5f);
            playerMovement.enabled = true;
            levelToEnable.SetActive(true);
            for (int i = 0; i < objectsToEnable.Length; ++i) {
                objectsToEnable[i].SetActive(true);
            }
        } else {
            Debug.Log("LevelComplete");
        }
        gameObject.SetActive(false);
    }
}
