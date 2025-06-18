using TMPro;
using UnityEngine;

public class RosterUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rosterUIPrefab;

    private void OnDisable()
    {
        GameManager.Instance.Roster.OnAddWorker -= GenerateWorkerUI;
    }

    private void Start()
    {
        GameManager.Instance.Roster.OnAddWorker += GenerateWorkerUI;
    }

    private void GenerateWorkerUI(Worker worker)
    {
        RectTransform workerUI = Instantiate(_rosterUIPrefab, this.transform);
        workerUI.GetComponentInChildren<TextMeshProUGUI>().SetText($"{worker.Name}: Idle");
    }
}