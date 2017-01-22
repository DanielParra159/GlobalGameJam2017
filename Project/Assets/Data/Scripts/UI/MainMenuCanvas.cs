using UnityEngine;

using DG.Tweening;

public sealed class MainMenuCanvas : MonoBehaviour {
    public static MainMenuCanvas Instance {
        get;
        private set;
    }

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private AudioSource menuMusic;
    [SerializeField]
    private AudioSource gameMusic;

    [SerializeField]
    private AudioSource select;

    private void Awake() {
        Instance = this;
        canvasGroup.alpha = 1.0f;
        gameObject.SetActive(true);
    }

    private void Start() {
        Movement.Instance.enabled = false;
    }

    public void Active(bool active, float fadeSpeed) {
        if (active) {
            Movement.Instance.enabled = false;
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0.0f;

            gameObject.SetActive(true);
            menuMusic.Stop();
            menuMusic.Play();
            menuMusic.DOFade(1.0f, fadeSpeed);
            gameMusic.DOFade(0.0f, fadeSpeed);
            

            canvasGroup.DOFade(1.0f, fadeSpeed).SetUpdate(true).OnComplete(OnCompleteActive);
        } else {
            canvasGroup.interactable = false;
            canvasGroup.DOFade(0.0f, fadeSpeed).SetUpdate(true).OnComplete(OnCompleteDeactivate);
            menuMusic.DOFade(0.0f, fadeSpeed);
            gameMusic.DOFade(1.0f, fadeSpeed);
            gameMusic.Play();
        }
    }

    private void OnCompleteActive() {
        canvasGroup.interactable = true;
    }
    private void OnCompleteDeactivate() {
        gameObject.SetActive(false);
        Movement.Instance.enabled = true;
        Movement.Instance.Reset();
        MainCamara.Instance.FollowPlayer = true;
    }

    public void OnStart() {
        select.Play();
        Active(false, 0.5f);
    }

    public void OnExit() {
        select.Play();
        Application.Quit();
    }

}
