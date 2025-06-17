using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlotUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI _producerTMP, _yieldTMP, _processTMP;
    private Plot _plot = new();

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_plot.IsPlanting) return;

        StartCoroutine(_plot.IE_Plant(UpdateProducerTMP, UpdateYieldTMP, UpdateProcessTMP));
    }

    private void UpdateProducerTMP(Seed seed)
    {
        _producerTMP.SetText(seed.Name);
    } 

    private void UpdateYieldTMP(int yield, int maxYield)
    {
        _yieldTMP.SetText($"{yield}/{maxYield}");
    }

    private void UpdateProcessTMP(float timeLeft)
    {
        _processTMP.SetText(Utils.TimeFormatter(timeLeft));
    }
}