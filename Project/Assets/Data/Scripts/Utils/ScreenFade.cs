using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace Common.Utils {
    [RequireComponent(typeof(Image))]
    public sealed class ScreenFade : MonoBehaviour {
        private static ScreenFade instance = null;
        public static ScreenFade Instance {
            get { return instance; }
        }

        private Image sprite;
        public bool Launched {
            get;
            private set;
        }

        private void Awake() {
            instance = this;
            sprite = GetComponent<Image>();
            sprite.enabled = false;
        }

        public void StopFade() {
            sprite.DOKill(false);
        }

        public void DoFade(Color color, float fadeDuration) {
            if (Launched)
                sprite.DOKill(false);
            else
                Launched = true;
            sprite.enabled = true;
            sprite.color = new Color(color.r, color.g, color.b, sprite.color.a);

            sprite.DOFade(color.a, fadeDuration).OnComplete(() => EndFade(color)).SetUpdate(true);
        }

        public void EndFade(Color color) {
            if (color.a == 0)
                sprite.enabled = false;
            Launched = false;
        }

    }
}