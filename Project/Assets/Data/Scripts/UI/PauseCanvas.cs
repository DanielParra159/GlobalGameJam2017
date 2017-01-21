using UnityEngine;

using DG.Tweening;

public sealed class PauseCanvas : MonoBehaviour {
    public static PauseCanvas Instance {
        get;
        private set;
    }

    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Awake() {
        Instance = this;
        canvasGroup.DOFade(0.0f, 0.0f);
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        Time.timeScale = 0.0f;
        canvasGroup.DOFade(1.0f, 0.2f).SetUpdate(true);
    }

    private void OnDisable() {
        Time.timeScale = 1.0f;
        canvasGroup.DOFade(0.0f, 0.0f).SetUpdate(true);
    }

    public void OnMenu() {
        Time.timeScale = 1.0f;
        canvasGroup.DOFade(0.0f, 0.5f).SetUpdate(false).OnComplete(()=> {
            gameObject.SetActive(false);
        });
        MainMenuCanvas.Instance.Active(true, 0.5f);
    }

    public void OnResume() {
        Time.timeScale = 1.0f;
        canvasGroup.DOFade(0.0f, 0.5f).SetUpdate(false).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }
}
