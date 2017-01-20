using UnityEngine;
using System.Collections;

namespace Common.Utils
{
    public sealed class DontDestroyOnLoad : MonoBehaviour {

		private void Awake () {
            DontDestroyOnLoad(gameObject);
            this.enabled = false;
		}
		
	}
}
