using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _producerTMP, _yieldTMP, _processTMP;
    [SerializeField] private Button _harvestButton, _upgradeButton;
    [SerializeField] private RectTransform _producePanel, _plantPanel, _lockPanel;
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

        HandleShowPanelOnPlotState(_plot.State);
        UpdateProducerTMP(_plot.Name);

        _plot.OnStateChange += HandleShowPanelOnPlotState;

        _plot.OnTimer += UpdateProcessTMP;
        _plot.OnYieldChange += UpdateYieldTMP;
        _plot.OnDecay += ResetUI;

        InitializeLockPanel();
        InitializePlantPanel();
        InitializeProducePanel();
    }

    private void HandleShowPanelOnPlotState(EPlotState state)
    {
        ShowPanelIf(_plantPanel, state == EPlotState.EMPTY);
        ShowPanelIf(_producePanel, state == EPlotState.PLANTED || state == EPlotState.DECAY);
        ShowPanelIf(_lockPanel, state == EPlotState.LOCK);
    }

    private void InitializeLockPanel()
    {
        Button unlockButton = _lockPanel.GetComponentInChildren<Button>();
        unlockButton.onClick.AddListener(() => _plot.TryBuy(GameManager.Instance.Wallet, FarmManager.Instance));

        _lockPanel.GetComponentInChildren<TextMeshProUGUI>().SetText($"Unlock for {_plot.Price}");
    }

    private void InitializePlantPanel()
    {
        Button produceButton = _plantPanel.GetComponentInChildren<Button>();
        produceButton.onClick.AddListener(() => HandleSeedButtonPressed());

        _plantPanel.GetComponentInChildren<TextMeshProUGUI>().SetText(_plot.Name);
    }

    private void InitializeProducePanel()
    {
        _upgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Upgrade for {_plot.UpgradeCost}");
    }

    private void HandleSeedButtonPressed()
    {
        _plot.PlantSeed(FarmManager.Instance.Inventory, () => FloatingTextUI.Instance.ShowText("Inventory has no producer item."));
    }

    private void HandleHarvestButtonPressed()
    {
        if (!_plot.TryHarvest(FarmManager.Instance.Inventory))
        {
            FloatingTextUI.Instance.ShowText("Can not harvest in this time.");                
        }
    }

    private void HandleUpgradeButtonPressed()
    {
        _plot.Upgrade(GameManager.Instance.Wallet);
    }

    private void ResetUI()
    {
        _yieldTMP.SetText("0/0");
        _processTMP.SetText("0");
    }

    private void UpdateProducerTMP(string name)
    {
        _producerTMP.SetText(name);
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