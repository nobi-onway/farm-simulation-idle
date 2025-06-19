using UnityEngine;

public class FarmUI : MonoBehaviour
{
    [SerializeField] private PlotUI _plotUIPrefab;
    [SerializeField] private RectTransform _plotContainer;

    private void Start()
    {
        foreach (Plot plot in FarmManager.Instance.Plots)
        {
            GeneratePlotUI(plot);
        }
    }

    private void GeneratePlotUI(Plot plot)
    {
        PlotUI plotUIClone = Instantiate(_plotUIPrefab, _plotContainer);
        plotUIClone.Initialize(plot);
    }
}