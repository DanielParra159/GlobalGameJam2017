﻿using UnityEngine;

using DG.Tweening;

public sealed class MainMenuCanvas : MonoBehaviour {
    public static MainMenuCanvas Instance {
        get;
        private set;
    }

    [SerializeField]
    private CanvasGroup canvasGroup;

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
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0.0f;

            gameObject.SetActive(true);

            canvasGroup.DOFade(1.0f, fadeSpeed).SetUpdate(true).OnComplete(OnCompleteActive);
        } else {
            canvasGroup.interactable = false;
            canvasGroup.DOFade(0.0f, fadeSpeed).SetUpdate(true).OnComplete(OnCompleteDeactivate);
        }
    }

    private void OnCompleteActive() {
        canvasGroup.interactable = true;
    }
    private void OnCompleteDeactivate() {
        gameObject.SetActive(false);
        Movement.Instance.enabled = true;
    }

    public void OnStart() {
        Active(false, 0.5f);
    }

    public void OnExit() {
        Application.Quit();
    }

}
