using UnityEngine;

public class FarmUI : MonoBehaviour
{
    [SerializeField] private PlotUI _plotUIPrefab;
    [SerializeField] private RectTransform _plotContainer;

    private void Start()
    {
        foreach (PlotData data in ResourceManager.Instance.PlotDataLookup.Values)
        {
            Product product = new(ResourceManager.Instance.ProductDataLookUp[data.ProducerId]);

            GeneratePlotUI(new Plot(data, product));
        }
    }

    private void GeneratePlotUI(Plot plot)
    {
        PlotUI plotUIClone = Instantiate(_plotUIPrefab, _plotContainer);
        plotUIClone.Initialize(plot);
    }
}