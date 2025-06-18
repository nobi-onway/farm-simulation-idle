using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _producerTMP, _yieldTMP, _processTMP;
    [SerializeField] private Button _harvestButton, _upgradeButton;
    [SerializeField] private RectTransform _producePanel, _plantPanel;
    [SerializeField] private Button _seedButtonPrefab;

    private Plot _plot;

    private void OnEnable()
    {
        _harvestButton.onClick.AddListener(HandleHarvestButtonPressed);
        _upgradeButton.onClick.AddListener(HandleUpgradeButtonPressed);
    }

    private void OnDisable()
    {
        _harvestButton.onClick.RemoveListener(HandleHarvestButtonPressed);
        _upgradeButton.onClick.RemoveListener(HandleUpgradeButtonPressed);
    }

    public void Initialize(Plot plot)
    {
        _plot = plot;

        _plot.OnStateChange += (state) =>
        {
            ShowPanelIf(_plantPanel, state == EPlotState.EMPTY);
            ShowPanelIf(_producePanel, state == EPlotState.PLANTED);
        };

        _plot.OnTimer += UpdateProcessTMP;
        _plot.OnYieldChange += UpdateYieldTMP;
        _plot.OnPlant += UpdateProducerTMP;
        _plot.OnDecay += ResetUI;

        InitializePlantPanel();
    }

    private void InitializePlantPanel()
    {
        _plantPanel.gameObject.SetActive(true);

        Button seedButton = Instantiate(_seedButtonPrefab, _plantPanel);
        seedButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Start Producer");
        seedButton.onClick.AddListener(() => HandleSeedButtonPressed());
    }

    private void HandleSeedButtonPressed()
    {
        _plot.PlantSeed(GameManager.Instance.Inventory);
    }

    private void HandleHarvestButtonPressed()
    {
        _plot.Harvest();
    }

    private void HandleUpgradeButtonPressed()
    {
        _plot.Upgrade();
    }

    private void ResetUI()
    {
        _producerTMP.SetText("None");
        _yieldTMP.SetText("0/0");
        _processTMP.SetText("0");
    }

    private void UpdateProducerTMP(ProducerItem seed)
    {
        _producerTMP.SetText(seed.Name);
    }

    private void UpdateYieldTMP(int yield, int maxYield)
    {
        _yieldTMP.SetText($"{yield}/{maxYield}");
    }

    private void UpdateProcessTMP(float timeLeft)
    {
        _processTMP.SetText(FormatterUtils.TimeFormatter(timeLeft));
    }

    private void ShowPanelIf(RectTransform panel, bool canShow)
    {
        panel.gameObject.SetActive(canShow);
    }
}