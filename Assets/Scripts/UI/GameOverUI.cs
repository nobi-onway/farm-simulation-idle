using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnEndGame += ShowUI;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnEndGame -= ShowUI;
    }

    private void ShowUI()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }
}