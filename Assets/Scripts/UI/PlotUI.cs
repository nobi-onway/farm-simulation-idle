using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlotUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI _producerTMP, _yieldTMP, _processTMP;
    [SerializeField] private Button _harvestButton;

    private Plot _plot = new();

    private void OnEnable()
    {
        _harvestButton.onClick.AddListener(HandleHarvestButtonPressed);
    }

    private void OnDisable()
    {
        _harvestButton.onClick.RemoveListener(HandleHarvestButtonPressed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_plot.IsPlanting) return;

        StartCoroutine(_plot.IE_Plant(UpdateProducerTMP, UpdateYieldTMP, UpdateProcessTMP, ResetUI));
    }

    private void HandleHarvestButtonPressed()
    {
        if(!_plot.IsPlanting) return;
        
        _plot.Harvest();
    }

    private void ResetUI()
    {
        _producerTMP.SetText("None");
        _yieldTMP.SetText("0/0");
        _processTMP.SetText("0");
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