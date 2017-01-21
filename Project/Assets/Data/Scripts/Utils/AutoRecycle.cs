using UnityEngine;

namespace Common.Utils
{
    public class AutoRecycle : MonoBehaviour
    {
        [SerializeField]
        private float delay = 1.0f;

        private float timeToRecycle;

        // Use this for initialization
        /*private void Start()
        {
            Invoke("Recycle", delay);
        }*/

        private void OnEnable() {
            timeToRecycle = Time.time + delay;
        }

        private void Update() {
            if (Time.time > timeToRecycle)
                Recycle();
        }

        private void Recycle()
        {
            gameObject.RecyclePool();
        }
    }
}