using UnityEngine;

using Common.Utils;

[RequireComponent(typeof(BoxCollider))]
public sealed class Destructible : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer myRenderer;
    [SerializeField]
    private SpriteRenderer breakRenderer;

    [SerializeField]
    private GameObject explosionPrefab;

    private BoxCollider myBoxCollider;

    private void Awake() {
        myBoxCollider = gameObject.GetComponent<BoxCollider>();
        explosionPrefab.CreatePool(50);
        Reset();
    }

    // Use this for initialization
    public void Reset () {
        myRenderer.enabled = true;
        breakRenderer.enabled = false;
        myBoxCollider.enabled = true;

    }

    public void Explode() {
        explosionPrefab.SpawnPool(transform.position);
        myBoxCollider.enabled = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerDefinitions.PLAYER_PROJECTILE_LAYER) {
            Explode();
        }
    }
}
