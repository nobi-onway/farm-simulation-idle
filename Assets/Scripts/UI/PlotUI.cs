using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _producerTMP, _yieldTMP, _processTMP;
    [SerializeField] private Button _harvestButton, _upgradeButton;
    [SerializeField] private RectTransform _producePanel, _plantPanel;
    [SerializeField] private Button _seedButtonPrefab;

    private Plot _plot = new();

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

    private void Start()
    {
        InitializePlantPanel();
    }

    private void InitializePlantPanel()
    {
        _plantPanel.gameObject.SetActive(true);

        foreach (RectTransform child in _plantPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Seed seed in GameManager.Instance.Inventory.Seeds)
        {
            Button seedButton = Instantiate(_seedButtonPrefab, _plantPanel);
            seedButton.GetComponentInChildren<TextMeshProUGUI>().SetText(seed.Name);
            seedButton.onClick.AddListener(() => HandleSeedButtonPressed(seed));
        }
    }

    private void HandleSeedButtonPressed(Seed seed)
    {
        _plantPanel.gameObject.SetActive(false);
        _plot.PlantSeed(seed);

        _producePanel.gameObject.SetActive(true);
        StartCoroutine(_plot.IE_Plant(UpdateProducerTMP, UpdateYieldTMP, UpdateProcessTMP, ResetUI));
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

        _producePanel.gameObject.SetActive(false);
        InitializePlantPanel();
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