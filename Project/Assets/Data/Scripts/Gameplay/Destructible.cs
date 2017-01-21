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

    [SerializeField]
    private AudioSource explosion;

    private bool exploded = false;

    private void Awake() {
        explosionPrefab.CreatePool(50);
        Reset();
    }

    // Use this for initialization
    public void Reset () {
        myRenderer.enabled = true;
        breakRenderer.enabled = false;
        exploded = false;

    }

    public void Explode() {
        explosionPrefab.SpawnPool(transform.position);
        myRenderer.enabled = false;
        breakRenderer.enabled = true;
        MainCamara.Instance.Shake(1.0f);
        exploded = true;
        explosion.Play();
    }

    private void OnCollisionEnter(Collision collision) {
        if (!exploded && collision.gameObject.layer == LayerDefinitions.PLAYER_PROJECTILE_LAYER) {
            Explode();
        }
    }
}
