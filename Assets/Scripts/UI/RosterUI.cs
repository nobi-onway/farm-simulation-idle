using TMPro;
using UnityEngine;

public class RosterUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rosterUIPrefab;

    private void OnDisable()
    {
        FarmManager.Instance.Roster.OnAddWorker -= GenerateWorkerUI;
    }

    private void Start()
    {
        FarmManager.Instance.Roster.OnAddWorker += GenerateWorkerUI;
    }

    private void GenerateWorkerUI(Worker worker)
    {
        RectTransform workerUI = Instantiate(_rosterUIPrefab, this.transform);

        workerUI.GetComponentInChildren<TextMeshProUGUI>().SetText($"{worker.Name}: \n {worker.State}");

        worker.OnStateChange += state => workerUI.GetComponentInChildren<TextMeshProUGUI>().SetText($"{worker.Name}: \n {state}");
    }
}