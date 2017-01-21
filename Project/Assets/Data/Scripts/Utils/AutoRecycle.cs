using UnityEngine;

namespace Common.Utils
{
    public class AutoRecycle : MonoBehaviour
    {
        [SerializeField]
        private float delay = 1.0f;

        // Use this for initialization
        private void Start()
        {
            Invoke("Recycle", delay);
        }

        private void Recycle()
        {
            gameObject.RecyclePool();
        }
    }
}